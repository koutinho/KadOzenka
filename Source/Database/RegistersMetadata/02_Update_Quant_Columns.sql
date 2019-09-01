
-- II. Обновление таблиц QUANT
-- Перенос колонок показателя "Состояние адреса" (5200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_STATE')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_STATE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Состояние адреса - С.А." (5200101)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_STATE_NAME')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_STATE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_STATE_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_STATE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата состояния - С.А." (5200102)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_STATE_DATE')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_STATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус адреса" (5200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_STATUS_NAME')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_STATUS_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_STATUS_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_STATUS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Инд.номер" (5200300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'EMP_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Документ основание (Д.О.)" (5200400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Тип документа - Д.О." (5200401)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT_TYPE_NAME')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT_TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT_TYPE_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер документа - Д.О." (5200402)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT_NUMBER')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Дата выдачи документа - Д.О." (5200403)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT_DATE_ISSUE')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT_DATE_ISSUE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Содержание документа - Д.О." (5200404)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT_CONTENT_NAME')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT_CONTENT_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'GROUND_DOCUMENT_CONTENT_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add GROUND_DOCUMENT_CONTENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "UNAD" (5200500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'UNAD')) then
        execute 'alter table BTI_ADDRLINK_Q add UNAD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Адрес" (5200600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ADDRESS_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add ADDRESS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ID объекта" (5200700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'BUILDING_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add BUILDING_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Регистрационный номер в адресном реестре" (5200800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_NUMBER')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_NUMBER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата регистрации адреса" (5200900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DATE')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Тип документа-основания (рег)" (5201000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DOC_TYPE_NAME')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DOC_TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DOC_TYPE_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DOC_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер документа-основания (рег)" (5201100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DOC_NUMBER')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DOC_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Дата документа-основания (рег)" (5201200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DOC_DATE')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DOC_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Содержание документа-основания (рег)" (5201300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DOC_CONTENT_NAME')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DOC_CONTENT_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REG_DOC_CONTENT_ID')) then
        execute 'alter table BTI_ADDRLINK_Q add REG_DOC_CONTENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра объекта" (5201400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'REGISTER_OBJECT_NUMBER')) then
        execute 'alter table BTI_ADDRLINK_Q add REGISTER_OBJECT_NUMBER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий " (5201500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'COMMENTS')) then
        execute 'alter table BTI_ADDRLINK_Q add COMMENTS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Источник данных" (5201600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'TEXT_SOURCE')) then
        execute 'alter table BTI_ADDRLINK_Q add TEXT_SOURCE VARCHAR(250)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRLINK_Q', 'ID_SOURCE')) then
        execute 'alter table BTI_ADDRLINK_Q add ID_SOURCE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Инд.номер" (25100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'EMP_ID')) then
        execute 'alter table BTI_BUILDING_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "UNOM" (25100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'UNOM')) then
        execute 'alter table BTI_BUILDING_Q add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Класс строения" (25100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KL')) then
        execute 'alter table BTI_BUILDING_Q add KL VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KL_CODE')) then
        execute 'alter table BTI_BUILDING_Q add KL_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Назначение" (25100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'NAZ')) then
        execute 'alter table BTI_BUILDING_Q add NAZ VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'NAZ_CODE')) then
        execute 'alter table BTI_BUILDING_Q add NAZ_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Материал стен" (25100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'MST')) then
        execute 'alter table BTI_BUILDING_Q add MST VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'MST_CODE')) then
        execute 'alter table BTI_BUILDING_Q add MST_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Этажность максимальная" (25100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'ET')) then
        execute 'alter table BTI_BUILDING_Q add ET INT8';
    end if;
end $$;


-- Перенос колонок показателя "Год постройки" (25100700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'GDPOSTR')) then
        execute 'alter table BTI_BUILDING_Q add GDPOSTR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер (К.Н.)" (25100800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KAD_N')) then
        execute 'alter table BTI_BUILDING_Q add KAD_N VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Этажность минимальная" (25100900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'ET_MIN')) then
        execute 'alter table BTI_BUILDING_Q add ET_MIN INT8';
    end if;
end $$;


-- Перенос колонок показателя "Этажность подземная" (25101000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'ET_PDZ')) then
        execute 'alter table BTI_BUILDING_Q add ET_PDZ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь общая" (25101200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OPL')) then
        execute 'alter table BTI_BUILDING_Q add OPL NUMERIC(22, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Состояние строения" (25101400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SOST')) then
        execute 'alter table BTI_BUILDING_Q add SOST VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SOST_CODE')) then
        execute 'alter table BTI_BUILDING_Q add SOST_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата состояния" (25101500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'DTSOST')) then
        execute 'alter table BTI_BUILDING_Q add DTSOST TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Признак постройки до 1917 г." (25101900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'GDDO1917')) then
        execute 'alter table BTI_BUILDING_Q add GDDO1917 INT2';
    end if;
end $$;


-- Перенос колонок показателя "Признак памятника архитектуры" (25102400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PAMARC')) then
        execute 'alter table BTI_BUILDING_Q add PAMARC INT2';
    end if;
end $$;


-- Перенос колонок показателя "Признак Аварийное" (25102500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'AVARZD')) then
        execute 'alter table BTI_BUILDING_Q add AVARZD INT2';
    end if;
end $$;


-- Перенос колонок показателя "Дата решения об аварийности здания" (25102600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'DTAVARZD')) then
        execute 'alter table BTI_BUILDING_Q add DTAVARZD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Признак Самовольное возведение" (25102800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SAMOVOL')) then
        execute 'alter table BTI_BUILDING_Q add SAMOVOL INT2';
    end if;
end $$;


-- Перенос колонок показателя "Площадь общая жилых помещений" (25103000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OPL_G')) then
        execute 'alter table BTI_BUILDING_Q add OPL_G NUMERIC(22, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь общая нежилых помещений" (25103200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OPL_N')) then
        execute 'alter table BTI_BUILDING_Q add OPL_N NUMERIC(22, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь застройки" (25103400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'NARPL')) then
        execute 'alter table BTI_BUILDING_Q add NARPL NUMERIC(22, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Серия проекта" (25103600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SER')) then
        execute 'alter table BTI_BUILDING_Q add SER VARCHAR(1000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SER_CODE')) then
        execute 'alter table BTI_BUILDING_Q add SER_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Год переоборудования" (25104000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'GDPEREOB')) then
        execute 'alter table BTI_BUILDING_Q add GDPEREOB INT2';
    end if;
end $$;


-- Перенос колонок показателя "Год капитального ремонта" (25104100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'GDKAPREM')) then
        execute 'alter table BTI_BUILDING_Q add GDKAPREM INT2';
    end if;
end $$;


-- Перенос колонок показателя "Площадь общая с летними жилых помещений" (25104200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PPL')) then
        execute 'alter table BTI_BUILDING_Q add PPL NUMERIC(22, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Капитальность" (25104300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KAP')) then
        execute 'alter table BTI_BUILDING_Q add KAP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий " (25104500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KOMM')) then
        execute 'alter table BTI_BUILDING_Q add KOMM VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Источник данных" (25106600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SOURCE')) then
        execute 'alter table BTI_BUILDING_Q add SOURCE VARCHAR(250)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'SOURCE_ID')) then
        execute 'alter table BTI_BUILDING_Q add SOURCE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип строения" (25107900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KAT')) then
        execute 'alter table BTI_BUILDING_Q add KAT VARCHAR(250)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KAT_CODE')) then
        execute 'alter table BTI_BUILDING_Q add KAT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип здания" (25108700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OBJ_TYPE')) then
        execute 'alter table BTI_BUILDING_Q add OBJ_TYPE VARCHAR(250)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OBJ_TYPE_CODE')) then
        execute 'alter table BTI_BUILDING_Q add OBJ_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки сведений" (25108900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'DOWNLOAD_DATE')) then
        execute 'alter table BTI_BUILDING_Q add DOWNLOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Площадь нежилых подвалов" (25109100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PDVPL_N')) then
        execute 'alter table BTI_BUILDING_Q add PDVPL_N NUMERIC(22, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Процент износа" (25110000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'ACTPROC')) then
        execute 'alter table BTI_BUILDING_Q add ACTPROC NUMERIC(10, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Год установки процента износа" (25110100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'GDPROC')) then
        execute 'alter table BTI_BUILDING_Q add GDPROC INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь кровли" (25110200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KROVPL')) then
        execute 'alter table BTI_BUILDING_Q add KROVPL NUMERIC(25, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Количество пассажирских лифтов" (25110300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'LFPQ')) then
        execute 'alter table BTI_BUILDING_Q add LFPQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество грузопассажирских лифтов" (25110400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'LFGPQ')) then
        execute 'alter table BTI_BUILDING_Q add LFGPQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество грузовых лифтов" (25110500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'LFGQ')) then
        execute 'alter table BTI_BUILDING_Q add LFGQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество жилых помещений" (25110600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PMQ_G')) then
        execute 'alter table BTI_BUILDING_Q add PMQ_G INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество комнат в жилых помещениях" (25110700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KMQ_G')) then
        execute 'alter table BTI_BUILDING_Q add KMQ_G INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество квартир" (25110800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KWQ')) then
        execute 'alter table BTI_BUILDING_Q add KWQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Отметка. 1 - считать как корпус, 0 - нет. Отметка "Считать как корпус" - говорит, что это уникальный объект недвижимости. Иначе это часть другого объекта недвижимости, выделенная для целей статистического учёта." (25110900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PRKOR')) then
        execute 'alter table BTI_BUILDING_Q add PRKOR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь холодных помещений" (25111000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'HPL')) then
        execute 'alter table BTI_BUILDING_Q add HPL NUMERIC(11, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Количество электрических плит" (25111100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'ELEQ')) then
        execute 'alter table BTI_BUILDING_Q add ELEQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество газовых плит" (25111200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'GAZQ')) then
        execute 'alter table BTI_BUILDING_Q add GAZQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь балконов" (25111300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'BPL')) then
        execute 'alter table BTI_BUILDING_Q add BPL NUMERIC(25, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь лоджий" (25111400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'LPL')) then
        execute 'alter table BTI_BUILDING_Q add LPL NUMERIC(25, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Материал перекрытий" (25111500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PEREKR')) then
        execute 'alter table BTI_BUILDING_Q add PEREKR VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'PEREKR_CODE')) then
        execute 'alter table BTI_BUILDING_Q add PEREKR_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Материал кровли" (25111600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KROV')) then
        execute 'alter table BTI_BUILDING_Q add KROV VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'KROV_CODE')) then
        execute 'alter table BTI_BUILDING_Q add KROV_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Состояние отселения корпуса" (25111700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OTSKORP')) then
        execute 'alter table BTI_BUILDING_Q add OTSKORP VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_BUILDING_Q', 'OTSKORP_CODE')) then
        execute 'alter table BTI_BUILDING_Q add OTSKORP_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Инд.номер" (25300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'EMP_ID')) then
        execute 'alter table BTI_FLOOR_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ID объекта" (25300200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'BUILDING_ID')) then
        execute 'alter table BTI_FLOOR_Q add BUILDING_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип этажа" (25300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'TYPE_NAME')) then
        execute 'alter table BTI_FLOOR_Q add TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_FLOOR_Q', 'TYPE_ID')) then
        execute 'alter table BTI_FLOOR_Q add TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер этажа" (25300400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'FLOOR_NUMBER')) then
        execute 'alter table BTI_FLOOR_Q add FLOOR_NUMBER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер этажа п/п" (25300500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'FLOOR_NUMBER_PP')) then
        execute 'alter table BTI_FLOOR_Q add FLOOR_NUMBER_PP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь_пп" (25300600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'AREA_PP')) then
        execute 'alter table BTI_FLOOR_Q add AREA_PP INT8';
    end if;
end $$;


-- Перенос колонок показателя "GUID_пп" (25300700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'GUID_PP')) then
        execute 'alter table BTI_FLOOR_Q add GUID_PP VARCHAR(38)';
    end if;
end $$;


-- Перенос колонок показателя "Номер_этажа_пп" (25300800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'NUMBER_PP')) then
        execute 'alter table BTI_FLOOR_Q add NUMBER_PP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип_этажа_пп" (25300900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'TYPE_PP')) then
        execute 'alter table BTI_FLOOR_Q add TYPE_PP VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Признак Подземный" (25301000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'IS_UNDEGROUND')) then
        execute 'alter table BTI_FLOOR_Q add IS_UNDEGROUND INT2';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра" (25301100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'REGISTER_OBJECT_NUMBER')) then
        execute 'alter table BTI_FLOOR_Q add REGISTER_OBJECT_NUMBER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наличие поэтажного плана" (25301200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_FLOOR_Q', 'FLOOR_PLAN_PRESENCE')) then
        execute 'alter table BTI_FLOOR_Q add FLOOR_PLAN_PRESENCE INT2';
    end if;
end $$;


-- Перенос колонок показателя "Инд.номер" (25400100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'EMP_ID')) then
        execute 'alter table BTI_PREMASE add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер (К.Н.)" (25400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'KADASTR')) then
        execute 'alter table BTI_PREMASE add KADASTR VARCHAR(64)';
    end if;
end $$;


-- Перенос колонок показателя "Этаж" (25400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'FLOOR_ID')) then
        execute 'alter table BTI_PREMASE add FLOOR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата обследования" (25400400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'INSPECTION_DATE')) then
        execute 'alter table BTI_PREMASE add INSPECTION_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный идентификатор помещения в рамках строения" (25400700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'UNKV')) then
        execute 'alter table BTI_PREMASE add UNKV INT8';
    end if;
end $$;


-- Перенос колонок показателя "Класс помещения" (25400800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'CLASS_NAME')) then
        execute 'alter table BTI_PREMASE add CLASS_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_PREMASE', 'CLASS_ID')) then
        execute 'alter table BTI_PREMASE add CLASS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип помещения" (25400900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'TYPE_NAME')) then
        execute 'alter table BTI_PREMASE add TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_PREMASE', 'TYPE_ID')) then
        execute 'alter table BTI_PREMASE add TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общая площадь помещения" (25401000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'TOTAL_AREA')) then
        execute 'alter table BTI_PREMASE add TOTAL_AREA INT8';
    end if;
end $$;


-- Перенос колонок показателя "Жилая площадь" (25401001)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'LIVING_AREA')) then
        execute 'alter table BTI_PREMASE add LIVING_AREA INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общая площадь (с летними)" (25401002)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'TOTAL_AREA_WITH_SUMMER')) then
        execute 'alter table BTI_PREMASE add TOTAL_AREA_WITH_SUMMER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Высота помещения" (25401400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'HEIGHT')) then
        execute 'alter table BTI_PREMASE add HEIGHT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер помещения" (25401700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'KVNOM')) then
        execute 'alter table BTI_PREMASE add KVNOM VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Номер секции " (25401800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'SECTION_NUMBER')) then
        execute 'alter table BTI_PREMASE add SECTION_NUMBER INT4';
    end if;
end $$;


-- Перенос колонок показателя "ID объекта в системе источнике" (25403300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'ID_IN_SOURCE')) then
        execute 'alter table BTI_PREMASE add ID_IN_SOURCE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество жилых комнат" (25403400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'ROOMS_COUNT')) then
        execute 'alter table BTI_PREMASE add ROOMS_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата обновления" (25403500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'UPDATE_DATE')) then
        execute 'alter table BTI_PREMASE add UPDATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Тип этажа" (25403600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'TET')) then
        execute 'alter table BTI_PREMASE add TET VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_PREMASE', 'TET_CODE')) then
        execute 'alter table BTI_PREMASE add TET_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип объекта недвижимости" (25403700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'OBJ_TYPE')) then
        execute 'alter table BTI_PREMASE add OBJ_TYPE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_PREMASE', 'OBJ_TYPE_CODE')) then
        execute 'alter table BTI_PREMASE add OBJ_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "UNOM" (25403800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_PREMASE', 'UNOM')) then
        execute 'alter table BTI_PREMASE add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Инд.номер" (25700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'EMP_ID')) then
        execute 'alter table BTI_ROOMS add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Помещение" (25700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'PREMISE_ID')) then
        execute 'alter table BTI_ROOMS add PREMISE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Назначение комнаты" (25700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'PURPOSE_NAME')) then
        execute 'alter table BTI_ROOMS add PURPOSE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ROOMS', 'PURPOSE_ID')) then
        execute 'alter table BTI_ROOMS add PURPOSE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Специальное назначение комнаты" (25700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'SPECIAL_PURPOSE_NAME')) then
        execute 'alter table BTI_ROOMS add SPECIAL_PURPOSE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ROOMS', 'SPECIAL_PURPOSE_ID')) then
        execute 'alter table BTI_ROOMS add SPECIAL_PURPOSE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Вид площади комнаты" (25700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'AREA_KIND_NAME')) then
        execute 'alter table BTI_ROOMS add AREA_KIND_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ROOMS', 'AREA_KIND_ID')) then
        execute 'alter table BTI_ROOMS add AREA_KIND_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип площади комнаты" (25700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'AREA_TYPE_NAME')) then
        execute 'alter table BTI_ROOMS add AREA_TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ROOMS', 'AREA_TYPE_ID')) then
        execute 'alter table BTI_ROOMS add AREA_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Высота" (25700700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'HEIGHT')) then
        execute 'alter table BTI_ROOMS add HEIGHT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата обследования" (25700800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'SURVEY_DATE')) then
        execute 'alter table BTI_ROOMS add SURVEY_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Формула подсчета площади" (25700900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'AREA_CALCULATION_FORMULA')) then
        execute 'alter table BTI_ROOMS add AREA_CALCULATION_FORMULA VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь комнаты" (25701000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'AREA')) then
        execute 'alter table BTI_ROOMS add AREA INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер пп" (25701100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'NUMBER_PP')) then
        execute 'alter table BTI_ROOMS add NUMBER_PP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Этаж" (25701200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'FLOOR_ID')) then
        execute 'alter table BTI_ROOMS add FLOOR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер на плане" (25701300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'PLAN_NUMBER')) then
        execute 'alter table BTI_ROOMS add PLAN_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Номер для документов" (25701400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'DOCUMENT_NUMBER')) then
        execute 'alter table BTI_ROOMS add DOCUMENT_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Понижающий коэффициент" (25701500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'REDUCTION_RATIO_NAME')) then
        execute 'alter table BTI_ROOMS add REDUCTION_RATIO_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ROOMS', 'REDUCTION_RATIO_ID')) then
        execute 'alter table BTI_ROOMS add REDUCTION_RATIO_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "GUID_пп" (25701600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'GUID_PP')) then
        execute 'alter table BTI_ROOMS add GUID_PP VARCHAR(38)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь_пп" (25701700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'AREA_PP')) then
        execute 'alter table BTI_ROOMS add AREA_PP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер_комнаты_пп " (25701800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'NUMBER_ROOM_PP')) then
        execute 'alter table BTI_ROOMS add NUMBER_ROOM_PP VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Переоборудовано без разрешения" (25701900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'IS_REFITTED_WO_PERMISSION')) then
        execute 'alter table BTI_ROOMS add IS_REFITTED_WO_PERMISSION INT2';
    end if;
end $$;


-- Перенос колонок показателя "Общее имущество многоквартирного дома" (25702000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'IS_COMMON_PRORERTY_APPARTMENT')) then
        execute 'alter table BTI_ROOMS add IS_COMMON_PRORERTY_APPARTMENT INT2';
    end if;
end $$;


-- Перенос колонок показателя "Общее имущество кондоминиума" (25702100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'IS_COMMON_PRORERTY_CONDOMINIUM')) then
        execute 'alter table BTI_ROOMS add IS_COMMON_PRORERTY_CONDOMINIUM INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер" (25702200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'KADASTR_NUMBER')) then
        execute 'alter table BTI_ROOMS add KADASTR_NUMBER VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "ID объекта в системе источнике" (25702300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'ID_IN_SOURCE')) then
        execute 'alter table BTI_ROOMS add ID_IN_SOURCE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата обновления" (25702400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ROOMS', 'UPDATE_DATE')) then
        execute 'alter table BTI_ROOMS add UPDATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (32300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOC_BASE_TYPE', 'ID')) then
        execute 'alter table INSUR_DOC_BASE_TYPE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Документ-основание" (32300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOC_BASE_TYPE', 'DOCUMENT_BASE')) then
        execute 'alter table INSUR_DOC_BASE_TYPE add DOCUMENT_BASE VARCHAR(255) not null';
    end if;
end $$;


-- Перенос колонок показателя "Вид" (32300400)

-- Перенос колонок показателя "Порядок сортировки" (32300500)

-- Перенос колонок показателя "Признак, что дата обязательная для заполнения" (32300600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOC_BASE_TYPE', 'NEED_SET_DATE')) then
        execute 'alter table INSUR_DOC_BASE_TYPE add NEED_SET_DATE INT2';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (32500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE_PACKAGE', 'ID')) then
        execute 'alter table INSUR_INPUT_FILE_PACKAGE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Период" (32500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE_PACKAGE', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_INPUT_FILE_PACKAGE add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор округа" (32500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE_PACKAGE', 'OKRUG_ID')) then
        execute 'alter table INSUR_INPUT_FILE_PACKAGE add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество районов всего" (32500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE_PACKAGE', 'COUNT_DISTRICT')) then
        execute 'alter table INSUR_INPUT_FILE_PACKAGE add COUNT_DISTRICT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (36000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'ID')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор здания ЕГРН" (36000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'EHD_PARCEL_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add EHD_PARCEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор здания БТИ" (36000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'BTI_BUILDING_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add BTI_BUILDING_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта страхования МКД" (36000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'INSUR_BUILDING_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add INSUR_BUILDING_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (36000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'DATE_LOADED')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add DATE_LOADED TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Сообщение об ошибке" (36000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'ERROR_MESSAGE')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add ERROR_MESSAGE VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор ошибки в журнале" (36000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'ERROR_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add ERROR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак ошибки" (36000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'IS_ERROR')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add IS_ERROR INT4';
    end if;
end $$;


-- Перенос колонок показателя "Дата обновления объекта в источнике данных ЕГРН" (36000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'UPDATE_DATE_EHD')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add UPDATE_DATE_EHD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер" (36001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'CAD_NUM')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add CAD_NUM VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "УНОМ" (36001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'UNOM')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата обновления объекта в источнике данных БТИ" (36001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'UPDATE_DATE_BTI')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add UPDATE_DATE_BTI TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Количество неудачных попыток обновления объекта" (36001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_BUILDING', 'ERROR_ATTEMPTS_COUNT')) then
        execute 'alter table IMPORT_LOG_INSUR_BUILDING add ERROR_ATTEMPTS_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (36100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'ID')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор здания ЕГРН" (36100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'EHD_PARCEL_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add EHD_PARCEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор здания БТИ" (36100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'BTI_BUILDING_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add BTI_BUILDING_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта страхования МКД" (36100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'INSUR_BUILDING_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add INSUR_BUILDING_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (36100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'DATE_LOADED')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add DATE_LOADED TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Сообщение об ошибке" (36100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'ERROR_MESSAGE')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add ERROR_MESSAGE VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор ошибки в журнале" (36100700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'ERROR_ID')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add ERROR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак ошибки" (36100800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'IS_ERROR')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add IS_ERROR INT4';
    end if;
end $$;


-- Перенос колонок показателя "Дата обновления объекта в источнике данных БТИ" (36100900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'UPDATE_DATE_EHD')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add UPDATE_DATE_EHD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата обновления объекта в источнике данных ЕГРН" (36101000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'UPDATE_DATE_BTI')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add UPDATE_DATE_BTI TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер" (36101100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'CAD_NUM')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add CAD_NUM VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "УНОМ" (36101200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'UNOM')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество неудачных попыток обновления объекта" (36101300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('IMPORT_LOG_INSUR_FLAT_B', 'ERROR_ATTEMPTS_COUNT')) then
        execute 'alter table IMPORT_LOG_INSUR_FLAT_B add ERROR_ATTEMPTS_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (40300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'EMP_ID')) then
        execute 'alter table FIAS_ADDROBJ add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Статус последней исторической записи в жизненном цикле адресного объекта" (40300200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'ACTSTATUS')) then
        execute 'alter table FIAS_ADDROBJ add ACTSTATUS NUMERIC(2)';
    end if;
end $$;


-- Перенос колонок показателя "Глобальный уникальный идентификатор адресного объекта" (40300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'AOGUID')) then
        execute 'alter table FIAS_ADDROBJ add AOGUID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный идентификатор записи. Ключевое поле." (40300400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'AOID')) then
        execute 'alter table FIAS_ADDROBJ add AOID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Уровень адресного объекта" (40300500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'AOLEVEL')) then
        execute 'alter table FIAS_ADDROBJ add AOLEVEL NUMERIC(2)';
    end if;
end $$;


-- Перенос колонок показателя "Код района" (40300600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'AREACODE')) then
        execute 'alter table FIAS_ADDROBJ add AREACODE VARCHAR(3)';
    end if;
end $$;


-- Перенос колонок показателя "Код автономии" (40300700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'AUTOCODE')) then
        execute 'alter table FIAS_ADDROBJ add AUTOCODE VARCHAR(1)';
    end if;
end $$;


-- Перенос колонок показателя "Статус центра" (40300800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'CENTSTATUS')) then
        execute 'alter table FIAS_ADDROBJ add CENTSTATUS NUMERIC(2)';
    end if;
end $$;


-- Перенос колонок показателя "Код города" (40300900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'CITYCODE')) then
        execute 'alter table FIAS_ADDROBJ add CITYCODE VARCHAR(3)';
    end if;
end $$;


-- Перенос колонок показателя "Код адресного элемента одной строкой с признаком актуальности из классификационного кода" (40301000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'CODE')) then
        execute 'alter table FIAS_ADDROBJ add CODE VARCHAR(17)';
    end if;
end $$;


-- Перенос колонок показателя "Статус актуальности КЛАДР 4 (последние две цифры в коде)" (40301100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'CURRSTATUS')) then
        execute 'alter table FIAS_ADDROBJ add CURRSTATUS NUMERIC(2)';
    end if;
end $$;


-- Перенос колонок показателя "Окончание действия записи" (40301200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'ENDDATE')) then
        execute 'alter table FIAS_ADDROBJ add ENDDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Формализованное наименование" (40301300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'FORMALNAME')) then
        execute 'alter table FIAS_ADDROBJ add FORMALNAME VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Код ИФНС ФЛ" (40301400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'IFNSFL')) then
        execute 'alter table FIAS_ADDROBJ add IFNSFL VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Код ИФНС ЮЛ" (40301500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'IFNSUL')) then
        execute 'alter table FIAS_ADDROBJ add IFNSUL VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор записи  связывания с последующей исторической записью" (40301600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'NEXTID')) then
        execute 'alter table FIAS_ADDROBJ add NEXTID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Официальное наименование" (40301700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'OFFNAME')) then
        execute 'alter table FIAS_ADDROBJ add OFFNAME VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "ОКАТО" (40301800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'OKATO')) then
        execute 'alter table FIAS_ADDROBJ add OKATO VARCHAR(11)';
    end if;
end $$;


-- Перенос колонок показателя "ОКТМО" (40301900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'OKTMO')) then
        execute 'alter table FIAS_ADDROBJ add OKTMO VARCHAR(11)';
    end if;
end $$;


-- Перенос колонок показателя "Статус действия над записью – причина появления записи" (40302000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'OPERSTATUS')) then
        execute 'alter table FIAS_ADDROBJ add OPERSTATUS NUMERIC(2)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта родительского объекта" (40302100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'PARENTGUID')) then
        execute 'alter table FIAS_ADDROBJ add PARENTGUID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Код населенного пункта" (40302200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'PLACECODE')) then
        execute 'alter table FIAS_ADDROBJ add PLACECODE VARCHAR(3)';
    end if;
end $$;


-- Перенос колонок показателя "Код адресного элемента одной строкой без признака актуальности (последних двух цифр)" (40302300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'PLAINCODE')) then
        execute 'alter table FIAS_ADDROBJ add PLAINCODE VARCHAR(15)';
    end if;
end $$;


-- Перенос колонок показателя "Почтовый индекс" (40302400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'POSTALCODE')) then
        execute 'alter table FIAS_ADDROBJ add POSTALCODE VARCHAR(6)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор записи связывания с предыдушей исторической записью" (40302500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'PREVID')) then
        execute 'alter table FIAS_ADDROBJ add PREVID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Код региона" (40302600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'REGIONCODE')) then
        execute 'alter table FIAS_ADDROBJ add REGIONCODE VARCHAR(2)';
    end if;
end $$;


-- Перенос колонок показателя "Краткое наименование типа объекта" (40302700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'SHORTNAME')) then
        execute 'alter table FIAS_ADDROBJ add SHORTNAME VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Начало действия записи" (40302800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'STARTDATE')) then
        execute 'alter table FIAS_ADDROBJ add STARTDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код улицы" (40302900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'STREETCODE')) then
        execute 'alter table FIAS_ADDROBJ add STREETCODE VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Код территориального участка ИФНС ФЛ" (40303000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'TERRIFNSFL')) then
        execute 'alter table FIAS_ADDROBJ add TERRIFNSFL VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Код территориального участка ИФНС ЮЛ" (40303100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'TERRIFNSUL')) then
        execute 'alter table FIAS_ADDROBJ add TERRIFNSUL VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Дата  внесения (обновления) записи" (40303200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'UPDATEDATE')) then
        execute 'alter table FIAS_ADDROBJ add UPDATEDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код внутригородского района" (40303300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'CTARCODE')) then
        execute 'alter table FIAS_ADDROBJ add CTARCODE VARCHAR(3)';
    end if;
end $$;


-- Перенос колонок показателя "Код дополнительного адресообразующего элемента" (40303400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'EXTRCODE')) then
        execute 'alter table FIAS_ADDROBJ add EXTRCODE VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Код подчиненного дополнительного адресообразующего элемента" (40303500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'SEXTCODE')) then
        execute 'alter table FIAS_ADDROBJ add SEXTCODE VARCHAR(3)';
    end if;
end $$;


-- Перенос колонок показателя "Статус актуальности адресного объекта ФИАС на текущую дату" (40303600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'LIVESTATUS')) then
        execute 'alter table FIAS_ADDROBJ add LIVESTATUS NUMERIC(2)';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ на нормативный документ" (40303700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'NORMDOC')) then
        execute 'alter table FIAS_ADDROBJ add NORMDOC VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Код элемента планировочной структуры" (40303800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'PLANCODE')) then
        execute 'alter table FIAS_ADDROBJ add PLANCODE VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер" (40303900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'CADNUM')) then
        execute 'alter table FIAS_ADDROBJ add CADNUM VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Тип деления" (40304000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_ADDROBJ', 'DIVTYPE')) then
        execute 'alter table FIAS_ADDROBJ add DIVTYPE NUMERIC(1)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор ФИАС" (40400100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'AOGUID')) then
        execute 'alter table FIAS_HOUSE add AOGUID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор дома (GUID)" (40400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'HOUSEGUID')) then
        execute 'alter table FIAS_HOUSE add HOUSEGUID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор дома (ID)" (40400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'HOUSEID')) then
        execute 'alter table FIAS_HOUSE add HOUSEID VARCHAR(36)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (40400400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'COUNTER')) then
        execute 'alter table FIAS_HOUSE add COUNTER NUMERIC(4)';
    end if;
end $$;


-- Перенос колонок показателя "Номер дома" (40400500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'HOUSENUM')) then
        execute 'alter table FIAS_HOUSE add HOUSENUM VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Номер корпуса" (40400600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'BUILDNUM')) then
        execute 'alter table FIAS_HOUSE add BUILDNUM VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Номер строения" (40400700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'STRUCNUM')) then
        execute 'alter table FIAS_HOUSE add STRUCNUM VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Тип номера дома" (40400800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'ESTSTATUS')) then
        execute 'alter table FIAS_HOUSE add ESTSTATUS NUMERIC(1)';
    end if;
end $$;


-- Перенос колонок показателя "Тип номера строения" (40400900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FIAS_HOUSE', 'STRSTATUS')) then
        execute 'alter table FIAS_HOUSE add STRSTATUS NUMERIC(1)';
    end if;
end $$;


-- Перенос колонок показателя "id" (40700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'ID')) then
        execute 'alter table EHD_OLD_NUMBERS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "global_id" (40700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'GLOBAL_ID')) then
        execute 'alter table EHD_OLD_NUMBERS add GLOBAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "type" (40700300)

-- Перенос колонок показателя "number" (40700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'NUMBER')) then
        execute 'alter table EHD_OLD_NUMBERS add NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "date" (40700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'DATE')) then
        execute 'alter table EHD_OLD_NUMBERS add DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "organ" (40700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'ORGAN')) then
        execute 'alter table EHD_OLD_NUMBERS add ORGAN VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "register_id" (40700700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'REGISTER_ID')) then
        execute 'alter table EHD_OLD_NUMBERS add REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (40700800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_OLD_NUMBERS', 'LOAD_DATE')) then
        execute 'alter table EHD_OLD_NUMBERS add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Инд.номер" (50000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'EMP_ID')) then
        execute 'alter table BTI_ADDRESS_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Полное наименование" (50000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'FULL_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add FULL_NAME VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Краткое наименование" (50000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'SHORT_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add SHORT_NAME VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование для сортировки" (50000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'NAME_FOR_SORT')) then
        execute 'alter table BTI_ADDRESS_Q add NAME_FOR_SORT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Основное наименование" (50000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'MAIN_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add MAIN_NAME VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Основное наименование" (50000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'MAIN_NAME_PRINT')) then
        execute 'alter table BTI_ADDRESS_Q add MAIN_NAME_PRINT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Полное наименование" (50000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'FULL_NAME_PRINT')) then
        execute 'alter table BTI_ADDRESS_Q add FULL_NAME_PRINT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "ID в источнике данных" (50000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'ID_IN_DS')) then
        execute 'alter table BTI_ADDRESS_Q add ID_IN_DS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Источник данных" (50000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'DATA_SOURCE_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add DATA_SOURCE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'DATA_SOURCE_ID')) then
        execute 'alter table BTI_ADDRESS_Q add DATA_SOURCE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Субъект РФ" (50001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'SUBJECT_RF_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add SUBJECT_RF_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'SUBJECT_RF_ID')) then
        execute 'alter table BTI_ADDRESS_Q add SUBJECT_RF_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Округ" (50001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'OKRUG_ID')) then
        execute 'alter table BTI_ADDRESS_Q add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Район" (50001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'DISTRICT_ID')) then
        execute 'alter table BTI_ADDRESS_Q add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Городское/сельское поселение" (50001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'SETTLEMENT_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add SETTLEMENT_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'SETTLEMENT_ID')) then
        execute 'alter table BTI_ADDRESS_Q add SETTLEMENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Город/НП" (50001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'TOWN_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add TOWN_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'TOWN_ID')) then
        execute 'alter table BTI_ADDRESS_Q add TOWN_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Элемент планир. структуры" (50001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'PSE_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add PSE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'PSE_ID')) then
        execute 'alter table BTI_ADDRESS_Q add PSE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Улица" (50001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'STREET_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add STREET_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'STREET_ID')) then
        execute 'alter table BTI_ADDRESS_Q add STREET_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип владения" (50001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'PROPERTY_TYPE_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add PROPERTY_TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'PROPERTY_TYPE_ID')) then
        execute 'alter table BTI_ADDRESS_Q add PROPERTY_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер участка" (50001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'PLOT_NUMBER')) then
        execute 'alter table BTI_ADDRESS_Q add PLOT_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Дом" (50001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'HOUSE_NUMBER')) then
        execute 'alter table BTI_ADDRESS_Q add HOUSE_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Корпус" (50002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'KORPUS_NUMBER')) then
        execute 'alter table BTI_ADDRESS_Q add KORPUS_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Тип сооружения (адр)" (50002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'STRUCTURE_TYPE_NAME')) then
        execute 'alter table BTI_ADDRESS_Q add STRUCTURE_TYPE_NAME VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'STRUCTURE_TYPE_ID')) then
        execute 'alter table BTI_ADDRESS_Q add STRUCTURE_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Строение" (50002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'STRUCTURE_NUMBER')) then
        execute 'alter table BTI_ADDRESS_Q add STRUCTURE_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Литера" (50002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'LETTER_NUMBER')) then
        execute 'alter table BTI_ADDRESS_Q add LETTER_NUMBER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Описание местоположения" (50002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'LOCATION_DESC')) then
        execute 'alter table BTI_ADDRESS_Q add LOCATION_DESC VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Код ОКАТО" (50002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'OKATO_CODE')) then
        execute 'alter table BTI_ADDRESS_Q add OKATO_CODE VARCHAR(11)';
    end if;
end $$;


-- Перенос колонок показателя "Код КЛАДР" (50002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'KLADR_CODE')) then
        execute 'alter table BTI_ADDRESS_Q add KLADR_CODE VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Почтовый индекс" (50002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'INDEX_POSTAL')) then
        execute 'alter table BTI_ADDRESS_Q add INDEX_POSTAL VARCHAR(6)';
    end if;
end $$;


-- Перенос колонок показателя "Тип корпуса" (50002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'TYPE_CORPUS')) then
        execute 'alter table BTI_ADDRESS_Q add TYPE_CORPUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'TYPE_CORPUS_ID')) then
        execute 'alter table BTI_ADDRESS_Q add TYPE_CORPUS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД адреса в НСИ" (50003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'CODE_NSI')) then
        execute 'alter table BTI_ADDRESS_Q add CODE_NSI VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Иное" (50003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'OTHER')) then
        execute 'alter table BTI_ADDRESS_Q add OTHER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код ОКТМО" (50003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'OKTMO')) then
        execute 'alter table BTI_ADDRESS_Q add OKTMO VARCHAR(64)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'OKTMO_ID')) then
        execute 'alter table BTI_ADDRESS_Q add OKTMO_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Адрес для полнотекстового поиска" (50003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'FULL_MIX_ADDRESS')) then
        execute 'alter table BTI_ADDRESS_Q add FULL_MIX_ADDRESS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Код ОКТМО по ОКАТО" (50003400)

-- Перенос колонок показателя "Признак адреса" (50003500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'ADDRESS_OR_LOCATION')) then
        execute 'alter table BTI_ADDRESS_Q add ADDRESS_OR_LOCATION INT2';
    end if;
end $$;


-- Перенос колонок показателя "Код ФИАС" (50003600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('BTI_ADDRESS_Q', 'CODE_FIAS')) then
        execute 'alter table BTI_ADDRESS_Q add CODE_FIAS VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор записи" (60000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'ID')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата заявки" (60000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'APP_DATE')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add APP_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер заявки ДГИ" (60000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'APP_NAME')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add APP_NAME VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор заявки" (60000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'APP_ID')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add APP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус заявки" (60000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'APP_STATUS')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add APP_STATUS VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Дата документа" (60000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DOC_DATE')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DOC_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер документа" (60000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DOC_NAME')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DOC_NAME VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на образ документа" (60000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DOC_LINK')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DOC_LINK VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на образ файла подписи" (60000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'SIG_LINK')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add SIG_LINK VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор документа" (60001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'APP_DOC_ID')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add APP_DOC_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование типа документа" (60001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DOC_TYPE')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DOC_TYPE VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Дополнительные атрибуты документа" (60001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'CUSTOM_XML')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add CUSTOM_XML TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Номер ФЛС" (60001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'FLS')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add FLS VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Статус обработки (внутренний параметр)" (60001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'STATUS')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add STATUS VARCHAR(1024)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'STATUS_CODE')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания (внутренний параметр)" (60001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DATE_CREATE')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DATE_CREATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата подтверждения получения (внутренний параметр)" (60001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DATE_CONFIRM')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DATE_CONFIRM TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата выполнения (внутренний параметр)" (60001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DATE_PERFORM')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DATE_PERFORM TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер договора" (60001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'DOC_NUMBER')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add DOC_NUMBER VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование арендатора" (60001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'NAME')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add NAME VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Ошибка" (60002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'ERROR')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add ERROR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Описание ошибки" (60002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'ERROR_MESSAGE')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add ERROR_MESSAGE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор процесса" (60002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'REGID')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add REGID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор статуса заявки" (60002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'APP_STATUS_ID')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add APP_STATUS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИНН" (60002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'INN')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add INN VARCHAR(12)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование госуслуги" (60002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_REQUEST_REGISTRATION', 'TYPERUS')) then
        execute 'alter table SPD_REQUEST_REGISTRATION add TYPERUS VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (60100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип профиля запроса СПД" (60100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'SPD_PROFILE_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add SPD_PROFILE_ID VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (60100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'CREATE_DATE')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add CREATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий" (60100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'COMMENTS')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add COMMENTS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Дата отправки запроса в СПД" (60100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'SPD_SEND_DATE')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add SPD_SEND_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Сообщение" (60100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'MESSAGE')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add MESSAGE VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "ИД Ошибки" (60100700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'ERROR_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add ERROR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата заявки СПД" (60100800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'SPD_APP_DATE')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add SPD_APP_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ИД Заявки СПД" (60100900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'SPD_APP_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add SPD_APP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер заявки СПД" (60101000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'SPD_APP_NAME')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add SPD_APP_NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "ИД реессра связанного объекта" (60101100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'OBJECT_REGISTER_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add OBJECT_REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД связанного объекта" (60101200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'OBJECT_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД связанных объектов" (60101300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'OBJECT_IDS')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add OBJECT_IDS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "ИД реессра основного объекта" (60101400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'MAIN_OBJECT_REGISTER_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add MAIN_OBJECT_REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД основного объекта" (60101500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'MAIN_OBJECT_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add MAIN_OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД отправителя" (60101600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_CREATE_FULL_APP_LOG', 'USER_ID')) then
        execute 'alter table SPD_CREATE_FULL_APP_LOG add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ID" (65000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'ID')) then
        execute 'alter table SPD_DOC_AGREEMENT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "PlanId" (65000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'PLANID')) then
        execute 'alter table SPD_DOC_AGREEMENT add PLANID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Status" (65000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'STATUS')) then
        execute 'alter table SPD_DOC_AGREEMENT add STATUS VARCHAR(1024)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'STATUS_CODE')) then
        execute 'alter table SPD_DOC_AGREEMENT add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "CreateDate" (65000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'CREATEDATE')) then
        execute 'alter table SPD_DOC_AGREEMENT add CREATEDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ChangeDate" (65000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'CHANGEDATE')) then
        execute 'alter table SPD_DOC_AGREEMENT add CHANGEDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Spd_APPID" (65000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_APPID')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_APPID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Spd_APPDOCID" (65000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_APPDOCID')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_APPDOCID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Spd_DEFINITION" (65000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_DEFINITION')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_DEFINITION VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Spd_ISHAND" (65000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_ISHAND')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_ISHAND INT8';
    end if;
end $$;


-- Перенос колонок показателя "Spd_ISSOGL" (65001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_ISSOGL')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_ISSOGL INT8';
    end if;
end $$;


-- Перенос колонок показателя "Spd_NUM" (65001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_NUM')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_NUM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Spd_PODP" (65001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_PODP')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_PODP VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Spd_SOGLCODE" (65001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_SOGLCODE')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_SOGLCODE VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Spd_SOGLDATE" (65001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_SOGLDATE')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_SOGLDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Spd_USERID" (65001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'SPD_USERID')) then
        execute 'alter table SPD_DOC_AGREEMENT add SPD_USERID INT8';
    end if;
end $$;


-- Перенос колонок показателя "DocAttached" (65001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'DOCATTACHED')) then
        execute 'alter table SPD_DOC_AGREEMENT add DOCATTACHED INT2';
    end if;
end $$;


-- Перенос колонок показателя "UserID" (65001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_DOC_AGREEMENT', 'USERID')) then
        execute 'alter table SPD_DOC_AGREEMENT add USERID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ID" (65100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_USERSRD2SPD', 'ID')) then
        execute 'alter table SPD_USERSRD2SPD add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "SRD_User_Id" (65100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_USERSRD2SPD', 'SRD_USER_ID')) then
        execute 'alter table SPD_USERSRD2SPD add SRD_USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "SPD_User_Id" (65100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_USERSRD2SPD', 'SPD_USER_ID')) then
        execute 'alter table SPD_USERSRD2SPD add SPD_USER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "UserCategory" (65100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('SPD_USERSRD2SPD', 'USERCATEGORY')) then
        execute 'alter table SPD_USERSRD2SPD add USERCATEGORY VARCHAR(128) not null';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('SPD_USERSRD2SPD', 'USERCATEGORY_CODE')) then
        execute 'alter table SPD_USERSRD2SPD add USERCATEGORY_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (80900100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'ID')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Код типа отчета" (80900200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'CODE')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Внутреннее наименование типа отчета" (80900300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'INTERNAL_NAME')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add INTERNAL_NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование отчета" (80900400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'TITLE')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add TITLE VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя, создавшего отчет" (80900500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'USER_ID')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД реестра объекта, для которого создан отчет" (80900600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'OBJECT_REGISTER_ID')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add OBJECT_REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД объекта, для которого создан отчет" (80900700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'OBJECT_ID')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания отчета" (80900800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'CREATE_DATE')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add CREATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер отчета" (80900900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'REPORT_NUMBER')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add REPORT_NUMBER VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Комментарии к отчету" (80901000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'COMMENTS')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add COMMENTS VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Параметры отчета" (80901100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'PARAMETERS')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add PARAMETERS TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Статус" (80901200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'STATUS')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add STATUS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата окончания асинхронной выгрузки" (80901300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'END_DATE')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add END_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Результат сохранения отчета" (80901400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'RESULT_MESSAGE')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add RESULT_MESSAGE VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Признак удаления" (80901500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'IS_DELETED')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add IS_DELETED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Тип сохраненного файла" (80901600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'FILE_TYPE')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add FILE_TYPE VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Раздел документа" (80901700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('FM_REPORTS_SAVEDREPORT', 'SECTION')) then
        execute 'alter table FM_REPORTS_SAVEDREPORT add SECTION VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (85000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'ID')) then
        execute 'alter table DASHBOARDS_DASHBOARD add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя" (85000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'USER_ID')) then
        execute 'alter table DASHBOARDS_DASHBOARD add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип лайаута" (85000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'LAYOUT_TYPE')) then
        execute 'alter table DASHBOARDS_DASHBOARD add LAYOUT_TYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (85000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'NAME')) then
        execute 'alter table DASHBOARDS_DASHBOARD add NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Описание" (85000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'DESCRIPTION')) then
        execute 'alter table DASHBOARDS_DASHBOARD add DESCRIPTION VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Доступен всем" (85000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_DASHBOARD', 'ISCOMMON')) then
        execute 'alter table DASHBOARDS_DASHBOARD add ISCOMMON INT2';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (85100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'ID')) then
        execute 'alter table DASHBOARDS_PANEL add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД дашбоарда" (85100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'DASHBOARD_ID')) then
        execute 'alter table DASHBOARDS_PANEL add DASHBOARD_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Заголовок" (85100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'TITLE')) then
        execute 'alter table DASHBOARDS_PANEL add TITLE VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "индекс колонки, в которой расположена панель" (85100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'COLUMN_INDEX')) then
        execute 'alter table DASHBOARDS_PANEL add COLUMN_INDEX INT8';
    end if;
end $$;


-- Перенос колонок показателя "порядок внутри одной колонки" (85100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'ORDER_IN_COLUMN')) then
        execute 'alter table DASHBOARDS_PANEL add ORDER_IN_COLUMN INT8';
    end if;
end $$;


-- Перенос колонок показателя "тип панели" (85100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'PANEL_TYPE_ID')) then
        execute 'alter table DASHBOARDS_PANEL add PANEL_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "сериализованный XML, содержащий конкретные значения Dto" (85100700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL', 'SETTINGS')) then
        execute 'alter table DASHBOARDS_PANEL add SETTINGS TEXT';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (85200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'ID')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "наименование гаджета" (85200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'NAME')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "описание" (85200300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'DESCRIPTION')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add DESCRIPTION VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "ссылка на гаджет" (85200400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'URL')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add URL VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "наименование класса Dto" (85200500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_PANEL_TYPE', 'DTO_CLASS_FULL_NAME')) then
        execute 'alter table DASHBOARDS_PANEL_TYPE add DTO_CLASS_FULL_NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (85300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'ID')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя" (85300200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'USER_ID')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД дашбоарда по-умолчанию" (85300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('DASHBOARDS_USER_SETTINGS', 'DEFAULT_PANEL_ID')) then
        execute 'alter table DASHBOARDS_USER_SETTINGS add DEFAULT_PANEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор списка" (92000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'ID')) then
        execute 'alter table CORE_LIST add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование списка" (92000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'NAME')) then
        execute 'alter table CORE_LIST add NAME VARCHAR(500) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор представления реестра, в рамках которого был создан список" (92000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_LIST add REGISTER_VIEW_ID VARCHAR(200) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра, в котором находятся объекты конкретного списка" (92000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'REGISTER_ID')) then
        execute 'alter table CORE_LIST add REGISTER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор автора списка" (92000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'AUTHOR')) then
        execute 'alter table CORE_LIST add AUTHOR INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак общего фильтра" (92000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'ISCOMMON')) then
        execute 'alter table CORE_LIST add ISCOMMON INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Коментарий к списку" (92000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'LIST_COMMENT')) then
        execute 'alter table CORE_LIST add LIST_COMMENT VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Дата изменения" (92000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST', 'CHANGE_DATE')) then
        execute 'alter table CORE_LIST add CHANGE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта в списке" (92100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST_OBJECT', 'ID')) then
        execute 'alter table CORE_LIST_OBJECT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор списка" (92100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST_OBJECT', 'LIST_ID')) then
        execute 'alter table CORE_LIST_OBJECT add LIST_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта" (92100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LIST_OBJECT', 'OBJECT_ID')) then
        execute 'alter table CORE_LIST_OBJECT add OBJECT_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Номер (идентификатор) типа" (92400100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_COLUMN_TYPE', 'ID')) then
        execute 'alter table CORE_LAYOUT_COLUMN_TYPE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Код" (92400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_COLUMN_TYPE', 'CODE')) then
        execute 'alter table CORE_LAYOUT_COLUMN_TYPE add CODE VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (92400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_COLUMN_TYPE', 'NAME')) then
        execute 'alter table CORE_LAYOUT_COLUMN_TYPE add NAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (92500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'ID')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя" (92500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'USER_ID')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД представления реестра" (92500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add REGISTER_VIEW_ID VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Настройка быстрого фильтра" (92500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSREGVIEW', 'FAST_FILTER')) then
        execute 'alter table CORE_SRD_USERSETTINGSREGVIEW add FAST_FILTER VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Номер (идентификатор) реестра" (93000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'REGISTERID')) then
        execute 'alter table CORE_REGISTER add REGISTERID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Текстовый код реестра" (93000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'REGISTERNAME')) then
        execute 'alter table CORE_REGISTER add REGISTERNAME VARCHAR(80) not null';
    end if;
end $$;


-- Перенос колонок показателя "Пользовательское наименование реестра" (93000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'REGISTERDESCRIPTION')) then
        execute 'alter table CORE_REGISTER add REGISTERDESCRIPTION VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование таблицы allpri, если используется." (93000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'ALLPRI_TABLE')) then
        execute 'alter table CORE_REGISTER add ALLPRI_TABLE VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование таблицы object, если используется." (93000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'OBJECT_TABLE')) then
        execute 'alter table CORE_REGISTER add OBJECT_TABLE VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование таблицы quant" (93000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'QUANT_TABLE')) then
        execute 'alter table CORE_REGISTER add QUANT_TABLE VARCHAR(40)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование колонки таблицы quant, в которой сохраняются идентификаторы изменений" (93000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'TRACK_CHANGES_COLUMN')) then
        execute 'alter table CORE_REGISTER add TRACK_CHANGES_COLUMN VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Тип хранения данных в реестрах" (93000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'STORAGE_TYPE')) then
        execute 'alter table CORE_REGISTER add STORAGE_TYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование последовательности, которая используется для формирование идентификаторов объектов" (93000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'OBJECT_SEQUENCE')) then
        execute 'alter table CORE_REGISTER add OBJECT_SEQUENCE VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Признак виртуального реестра" (93001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'IS_VIRTUAL')) then
        execute 'alter table CORE_REGISTER add IS_VIRTUAL INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак наличия исторических записей в будущем" (93001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER', 'CONTAINS_QUANT_IN_FUTURE')) then
        execute 'alter table CORE_REGISTER add CONTAINS_QUANT_IN_FUTURE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Номер показателя" (93100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'ID')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование показателя" (93100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'NAME')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add NAME VARCHAR(300) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра" (93100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'REGISTERID')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add REGISTERID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип показателя" (93100400)

-- Перенос колонок показателя "Связь с родщительским составным показателем для дочерних показателей" (93100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'PARENTID')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add PARENTID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Связь со справочником" (93100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'REFERENCEID')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add REFERENCEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование колонки для хранения значения показателя в таблице quant" (93100700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'VALUE_FIELD')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add VALUE_FIELD VARCHAR(32)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование колонки для хранения справлчного кода показателя в таблице quant" (93100800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'CODE_FIELD')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add CODE_FIELD VARCHAR(32)';
    end if;
end $$;


-- Перенос колонок показателя "Шаблон для сборки значения показателя" (93100900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'VALUE_TEMPLATE')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add VALUE_TEMPLATE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Признак первичного ключа" (93101000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'PRIMARY_KEY')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add PRIMARY_KEY INT2';
    end if;
end $$;


-- Перенос колонок показателя "Признак использования показателя в основной раскладке" (93101100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'USER_KEY')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add USER_KEY INT2';
    end if;
end $$;


-- Перенос колонок показателя "XML виртуальной колонки" (93101200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'QSCOLUMN')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add QSCOLUMN TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Внутреннее (системное) имя" (93101300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'INTERNAL_NAME')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add INTERNAL_NAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Признак обнуляемости" (93101400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'IS_NULLABLE')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add IS_NULLABLE INT2';
    end if;
end $$;


-- Перенос колонок показателя "Описание" (93101500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'DESCRIPTION')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add DESCRIPTION VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Стиль по умолчанию" (93101600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_ATTRIBUTE', 'LAYOUT')) then
        execute 'alter table CORE_REGISTER_ATTRIBUTE add LAYOUT TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (93200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'ID')) then
        execute 'alter table CORE_REGISTER_RELATION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование связи (описание)" (93200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'NAME')) then
        execute 'alter table CORE_REGISTER_RELATION add NAME VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Реестр, на который идет ссылка (родительский реестр)" (93200300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'PARENTREGISTER')) then
        execute 'alter table CORE_REGISTER_RELATION add PARENTREGISTER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Реестр, который ссылается на родительский (дочерний реестр)" (93200400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'CHIELDREGISTER')) then
        execute 'alter table CORE_REGISTER_RELATION add CHIELDREGISTER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Мощность связи" (93200500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'CARDINALITY')) then
        execute 'alter table CORE_REGISTER_RELATION add CARDINALITY VARCHAR(4)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор показателя дочернего реестра, в котором хранится значение внешнего ключа" (93200600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_RELATION', 'KINDID')) then
        execute 'alter table CORE_REGISTER_RELATION add KINDID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор раскладки" (93300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'LAYOUTID')) then
        execute 'alter table CORE_LAYOUT add LAYOUTID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование раскладки" (93300200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'LAYOUTNAME')) then
        execute 'alter table CORE_LAYOUT add LAYOUTNAME VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий к раскладке" (93300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'LAYOUTCOMMENT')) then
        execute 'alter table CORE_LAYOUT add LAYOUTCOMMENT VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра" (93300400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'REGISTERID')) then
        execute 'alter table CORE_LAYOUT add REGISTERID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сортировка по умолчанию" (93300500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'DEFAULTSORT')) then
        execute 'alter table CORE_LAYOUT add DEFAULTSORT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак раскладки по умолчанию" (93300600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'PREFFERED')) then
        execute 'alter table CORE_LAYOUT add PREFFERED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Имя пользователя, создавшего раскладку" (93300700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'USERNAME')) then
        execute 'alter table CORE_LAYOUT add USERNAME VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (93300800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'CREATEDATE')) then
        execute 'alter table CORE_LAYOUT add CREATEDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Шаблон QSQuery" (93300900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'QSQUERY')) then
        execute 'alter table CORE_LAYOUT add QSQUERY TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Только уникальные записи" (93301000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'ISDISTINCT')) then
        execute 'alter table CORE_LAYOUT add ISDISTINCT INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип сортировки" (93301100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'ORDERTYPE')) then
        execute 'alter table CORE_LAYOUT add ORDERTYPE VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя, создавшего раскладку" (93301200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'USER_ID')) then
        execute 'alter table CORE_LAYOUT add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак общей раскладки" (93301300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'ISCOMMON')) then
        execute 'alter table CORE_LAYOUT add ISCOMMON INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внутреннее имя" (93301400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'INTERNAL_NAME')) then
        execute 'alter table CORE_LAYOUT add INTERNAL_NAME VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Признак использовния режима миникаротчек для раскладки" (93301500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'ENABLE_MINICARDS_MODE')) then
        execute 'alter table CORE_LAYOUT add ENABLE_MINICARDS_MODE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор представления реестра, в рамках которого была создана раскладка" (93301600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_LAYOUT add REGISTER_VIEW_ID VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "ИД домена нерегламентированной отчетности (признак запроса)" (93301700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT', 'AS_DOMAIN_ID')) then
        execute 'alter table CORE_LAYOUT add AS_DOMAIN_ID VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор (главный ключ)" (93500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'ID')) then
        execute 'alter table CORE_LAYOUT_DETAILS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор раскладки (внешний ключ)" (93500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'LAYOUTID')) then
        execute 'alter table CORE_LAYOUT_DETAILS add LAYOUTID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип колонки" (93500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'DETAILTYPE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add DETAILTYPE INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Позиция колонки" (93500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'ORDINAL')) then
        execute 'alter table CORE_LAYOUT_DETAILS add ORDINAL INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор показателя" (93500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'ATTRIBUTEID')) then
        execute 'alter table CORE_LAYOUT_DETAILS add ATTRIBUTEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор колонки для сортировки" (93500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'SORTBYATTRIBUTE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add SORTBYATTRIBUTE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор справочника" (93500700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'REFERENCEID')) then
        execute 'alter table CORE_LAYOUT_DETAILS add REFERENCEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Текст заголовка" (93500800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'HEADERTEXT')) then
        execute 'alter table CORE_LAYOUT_DETAILS add HEADERTEXT VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Ширина заголовка" (93500900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'HEADERWIDTH')) then
        execute 'alter table CORE_LAYOUT_DETAILS add HEADERWIDTH INT8';
    end if;
end $$;


-- Перенос колонок показателя "Видимость" (93501000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'VISIBLE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add VISIBLE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Форматирование" (93501100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'FORMAT')) then
        execute 'alter table CORE_LAYOUT_DETAILS add FORMAT VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Тип данных" (93501200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'DATATYPE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add DATATYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Выражение" (93501300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'EXPRESSION')) then
        execute 'alter table CORE_LAYOUT_DETAILS add EXPRESSION VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "SQL-выражение" (93501400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'SQLEXPRESSION')) then
        execute 'alter table CORE_LAYOUT_DETAILS add SQLEXPRESSION VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Текст TOTALTEXT" (93501500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'TOTALTEXT')) then
        execute 'alter table CORE_LAYOUT_DETAILS add TOTALTEXT VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Тип TOTALTYPE" (93501600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'TOTALTYPE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add TOTALTYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Стиль колонки, сериализованный в XML" (93501700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'STYLE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add STYLE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Стиль включен" (93501800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'ENABLESTYLE')) then
        execute 'alter table CORE_LAYOUT_DETAILS add ENABLESTYLE INT2';
    end if;
end $$;


-- Перенос колонок показателя "Выравнивание текста" (93501900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'TEXTALIGN')) then
        execute 'alter table CORE_LAYOUT_DETAILS add TEXTALIGN VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Объект типа QSColumn, сериализованный в XML" (93502000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_DETAILS', 'QSCOLUMN')) then
        execute 'alter table CORE_LAYOUT_DETAILS add QSCOLUMN TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта" (93600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'QRYID')) then
        execute 'alter table CORE_QRY add QRYID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование фильтра" (93600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'NAME')) then
        execute 'alter table CORE_QRY add NAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Описание фильтра" (93600300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'DESCRIPTION')) then
        execute 'alter table CORE_QRY add DESCRIPTION VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (93600500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'DATEFROM')) then
        execute 'alter table CORE_QRY add DATEFROM TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Признак отображения фильтра в панеле быстрого доступа" (93600600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'INLIST')) then
        execute 'alter table CORE_QRY add INLIST INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Для общих/личных запросов" (93600700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'QRY_USER')) then
        execute 'alter table CORE_QRY add QRY_USER VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра" (93600800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'REGISTERID')) then
        execute 'alter table CORE_QRY add REGISTERID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Условие QSCondition" (93600900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'QSCONDITION')) then
        execute 'alter table CORE_QRY add QSCONDITION TEXT';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя, создавшего фильтр" (93601000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'USER_ID')) then
        execute 'alter table CORE_QRY add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак общего фильтра" (93601100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'ISCOMMON')) then
        execute 'alter table CORE_QRY add ISCOMMON INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внутреннее имя" (93601200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'INTERNAL_NAME')) then
        execute 'alter table CORE_QRY add INTERNAL_NAME VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор представления реестра, в рамках которого была создана раскладка" (93601300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_QRY add REGISTER_VIEW_ID VARCHAR(200) not null';
    end if;
end $$;


-- Перенос колонок показателя "Автор" (93601400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY', 'AUTHOR')) then
        execute 'alter table CORE_QRY add AUTHOR VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта" (93700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'QRYFILTERID')) then
        execute 'alter table CORE_QRY_FILTER add QRYFILTERID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор фильтра" (93700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'QRYID')) then
        execute 'alter table CORE_QRY_FILTER add QRYID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор операции" (93700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'QRYOPERATIONID')) then
        execute 'alter table CORE_QRY_FILTER add QRYOPERATIONID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор показателя" (93700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'KINDELEMENTID')) then
        execute 'alter table CORE_QRY_FILTER add KINDELEMENTID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Условие" (93700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'CONDITION')) then
        execute 'alter table CORE_QRY_FILTER add CONDITION VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "И / ИЛИ" (93700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'ANDTRUEORFALSE')) then
        execute 'alter table CORE_QRY_FILTER add ANDTRUEORFALSE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Позиция" (93700700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'QRYPOSITION')) then
        execute 'alter table CORE_QRY_FILTER add QRYPOSITION INT8';
    end if;
end $$;


-- Перенос колонок показателя "Значение" (93700800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'VALUE')) then
        execute 'alter table CORE_QRY_FILTER add VALUE VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Показатель передается по ссылке" (93700900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'BYREF')) then
        execute 'alter table CORE_QRY_FILTER add BYREF INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Открывающая скобка" (93701000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'BRACKETSFIRST')) then
        execute 'alter table CORE_QRY_FILTER add BRACKETSFIRST VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Закрывающая скобка" (93701100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'BRACKETSCLOSE')) then
        execute 'alter table CORE_QRY_FILTER add BRACKETSCLOSE VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор справочника" (93701200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'REFERENCEID')) then
        execute 'alter table CORE_QRY_FILTER add REFERENCEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор спец. реестра" (93701300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'SPECIALREGISTERID')) then
        execute 'alter table CORE_QRY_FILTER add SPECIALREGISTERID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор спец. Показателя" (93701400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_FILTER', 'SPECIALATTRIBUTETYPE')) then
        execute 'alter table CORE_QRY_FILTER add SPECIALATTRIBUTETYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта" (93800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_OPERATION', 'QRYOPERATIONID')) then
        execute 'alter table CORE_QRY_OPERATION add QRYOPERATIONID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Описание операции" (93800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_OPERATION', 'DESCRIPTION')) then
        execute 'alter table CORE_QRY_OPERATION add DESCRIPTION VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "SQL-выражение" (93800300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_QRY_OPERATION', 'SQLSTATEMENT')) then
        execute 'alter table CORE_QRY_OPERATION add SQLSTATEMENT VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор блокировки" (93900100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'ID')) then
        execute 'alter table CORE_REGISTER_LOCK add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (93900200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'USERID')) then
        execute 'alter table CORE_REGISTER_LOCK add USERID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра" (93900300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'REGISTERID')) then
        execute 'alter table CORE_REGISTER_LOCK add REGISTERID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта" (93900400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'OBJECTID')) then
        execute 'alter table CORE_REGISTER_LOCK add OBJECTID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата блокировки объекта" (93900500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_LOCK', 'LOCKDATE')) then
        execute 'alter table CORE_REGISTER_LOCK add LOCKDATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Гланвый ключ" (94000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'ID')) then
        execute 'alter table CORE_SRD_AUDIT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор функции модуля (подсистемы)" (94000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'FUNCTION_ID')) then
        execute 'alter table CORE_SRD_AUDIT add FUNCTION_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (94000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'USER_ID')) then
        execute 'alter table CORE_SRD_AUDIT add USER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Время выполнения функции" (94000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'ACTIONTIME')) then
        execute 'alter table CORE_SRD_AUDIT add ACTIONTIME TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак успешного выполнения" (94000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'RESULT')) then
        execute 'alter table CORE_SRD_AUDIT add RESULT INT2';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий по выполнению функции" (94000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'RESULT_DESC')) then
        execute 'alter table CORE_SRD_AUDIT add RESULT_DESC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к данным сессии" (94000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'SESSION_ID')) then
        execute 'alter table CORE_SRD_AUDIT add SESSION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра связанного объекта" (94000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'OBJECT_REGISTER_ID')) then
        execute 'alter table CORE_SRD_AUDIT add OBJECT_REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор связанного объекта" (94000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'OBJECT_ID')) then
        execute 'alter table CORE_SRD_AUDIT add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор статуса объекта после выполнения действия" (94001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'OBJECT_STATUS_ID')) then
        execute 'alter table CORE_SRD_AUDIT add OBJECT_STATUS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор действия из внешней системы" (94001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'EXTERNAL_ID')) then
        execute 'alter table CORE_SRD_AUDIT add EXTERNAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра основного объекта" (94001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'MAIN_OBJECT_REGISTER_ID')) then
        execute 'alter table CORE_SRD_AUDIT add MAIN_OBJECT_REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор основного объекта" (94001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_AUDIT', 'MAIN_OBJECT_ID')) then
        execute 'alter table CORE_SRD_AUDIT add MAIN_OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор подразделения" (94100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'ID')) then
        execute 'alter table CORE_SRD_DEPARTMENT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Номер подразделения" (94100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'CODE')) then
        execute 'alter table CORE_SRD_DEPARTMENT add CODE VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование подразделения" (94100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'NAME')) then
        execute 'alter table CORE_SRD_DEPARTMENT add NAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Руководитель" (94100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'MANAGER')) then
        execute 'alter table CORE_SRD_DEPARTMENT add MANAGER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование подразделения в родительном падеже" (94100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'NAME_GENITIVE_CASE')) then
        execute 'alter table CORE_SRD_DEPARTMENT add NAME_GENITIVE_CASE VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Признак архива" (94100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_DEPARTMENT', 'IS_DELETED')) then
        execute 'alter table CORE_SRD_DEPARTMENT add IS_DELETED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (94200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'ID')) then
        execute 'alter table CORE_SRD_FUNCTION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование функции" (94200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'FUNCTIONNAME')) then
        execute 'alter table CORE_SRD_FUNCTION add FUNCTIONNAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Код функции" (94200300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'FUNCTIONTAG')) then
        execute 'alter table CORE_SRD_FUNCTION add FUNCTIONTAG VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор модуля (подсистемы) системы" (94200400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'PARENT_ID')) then
        execute 'alter table CORE_SRD_FUNCTION add PARENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Описание функции" (94200500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION', 'DESCRIPTION')) then
        execute 'alter table CORE_SRD_FUNCTION add DESCRIPTION VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (94500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'ID')) then
        execute 'alter table CORE_SRD_ROLE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование роли" (94500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'ROLENAME')) then
        execute 'alter table CORE_SRD_ROLE add ROLENAME VARCHAR(320)';
    end if;
end $$;


-- Перенос колонок показателя "Код роли роли" (94500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'ROLETAG')) then
        execute 'alter table CORE_SRD_ROLE add ROLETAG VARCHAR(320)';
    end if;
end $$;


-- Перенос колонок показателя "Признак административной роли" (94500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'ISADMIN')) then
        execute 'alter table CORE_SRD_ROLE add ISADMIN INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак доступа на чтение из всех риеестров" (94500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'ALL_REGISTERS_READ')) then
        execute 'alter table CORE_SRD_ROLE add ALL_REGISTERS_READ INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак доступа на запись во все риеестры" (94500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'ALL_REGISTERS_WRITE')) then
        execute 'alter table CORE_SRD_ROLE add ALL_REGISTERS_WRITE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Подсистема" (94500700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE', 'SUBSYSTEM')) then
        execute 'alter table CORE_SRD_ROLE add SUBSYSTEM VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор связи" (94600000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FUNCTION', 'ID')) then
        execute 'alter table CORE_SRD_ROLE_FUNCTION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к функции" (94600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FUNCTION', 'FUNCTION_ID')) then
        execute 'alter table CORE_SRD_ROLE_FUNCTION add FUNCTION_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к роли" (94600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FUNCTION', 'ROLE_ID')) then
        execute 'alter table CORE_SRD_ROLE_FUNCTION add ROLE_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (94700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'ID')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к роли" (94700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'ROLE_ID')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add ROLE_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к реестрам" (94700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'REGISTER_ID')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add REGISTER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак права на чтение из реестра" (94700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'CAN_READ')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add CAN_READ INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак права на запись в реестр" (94700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'CAN_WRITE')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add CAN_WRITE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что права доступа относятся ко всем атрибутам" (94700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_REGISTER', 'ALL_ATTRIBUTES')) then
        execute 'alter table CORE_SRD_ROLE_REGISTER add ALL_ATTRIBUTES INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (94800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'ID')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор права доступа к реестру" (94800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'RULE_ID')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add RULE_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор показателя реестра" (94800300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'ATTRIBUTE_ID')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add ATTRIBUTE_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак права на чтение из атрибута реестра" (94800400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'CAN_READ')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add CAN_READ INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак права на запись в атрибут реестра" (94800500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_ATTR', 'CAN_WRITE')) then
        execute 'alter table CORE_SRD_ROLE_ATTR add CAN_WRITE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (94900100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'ID')) then
        execute 'alter table CORE_SRD_SESSION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (94900200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'USER_ID')) then
        execute 'alter table CORE_SRD_SESSION add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Время входа в систему" (94900300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'LOGINTIME')) then
        execute 'alter table CORE_SRD_SESSION add LOGINTIME TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Время выхода из системы" (94900400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'LOGOUTTIME')) then
        execute 'alter table CORE_SRD_SESSION add LOGOUTTIME TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "IP адрес клиентской машины" (94900500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'IP')) then
        execute 'alter table CORE_SRD_SESSION add IP VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор сессии ASP.NET" (94900600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'ASP_SESSION_ID')) then
        execute 'alter table CORE_SRD_SESSION add ASP_SESSION_ID VARCHAR(40)';
    end if;
end $$;


-- Перенос колонок показателя "Название браузера" (94900700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'BROWSER_NAME')) then
        execute 'alter table CORE_SRD_SESSION add BROWSER_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Версия браузера" (94900800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'BROWSER_VERSION')) then
        execute 'alter table CORE_SRD_SESSION add BROWSER_VERSION VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Платформа" (94900900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'BROWSER_PLATFORM')) then
        execute 'alter table CORE_SRD_SESSION add BROWSER_PLATFORM VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Версия Java Script" (94901000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'BROWSER_JS_VERSION')) then
        execute 'alter table CORE_SRD_SESSION add BROWSER_JS_VERSION VARCHAR(5)';
    end if;
end $$;


-- Перенос колонок показателя "Ошибка при входе" (94901100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'LOGIN_STATUS')) then
        execute 'alter table CORE_SRD_SESSION add LOGIN_STATUS INT2';
    end if;
end $$;


-- Перенос колонок показателя "Примечание" (94901200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'COMMENTARY')) then
        execute 'alter table CORE_SRD_SESSION add COMMENTARY VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Время последней активности" (94901300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_SESSION', 'LAST_ACTIVITY')) then
        execute 'alter table CORE_SRD_SESSION add LAST_ACTIVITY TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (95000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'ID')) then
        execute 'alter table CORE_SRD_USER add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к подразделению организации" (95000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'DEPARTMENT_ID')) then
        execute 'alter table CORE_SRD_USER add DEPARTMENT_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Имя пользователя (login)" (95000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'USERNAME')) then
        execute 'alter table CORE_SRD_USER add USERNAME VARCHAR(68) not null';
    end if;
end $$;


-- Перенос колонок показателя "Полное имя пользователя" (95000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'FULLNAME')) then
        execute 'alter table CORE_SRD_USER add FULLNAME VARCHAR(100) not null';
    end if;
end $$;


-- Перенос колонок показателя "Имя пользователя" (95000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'NAME')) then
        execute 'alter table CORE_SRD_USER add NAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Фамилия пользователя" (95000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'SURNAME')) then
        execute 'alter table CORE_SRD_USER add SURNAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Отчество пользователя" (95000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'PATRONYMIC')) then
        execute 'alter table CORE_SRD_USER add PATRONYMIC VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Полное имя пользователя в родительном падеже" (95000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'FULLNAME_FOR_DOC')) then
        execute 'alter table CORE_SRD_USER add FULLNAME_FOR_DOC VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Должность" (95000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'POSITION')) then
        execute 'alter table CORE_SRD_USER add POSITION VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Признак архивности учетной записи" (95001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'IS_DELETED')) then
        execute 'alter table CORE_SRD_USER add IS_DELETED INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата последнего изменения учетной записи" (95001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'CHANGE_DATE')) then
        execute 'alter table CORE_SRD_USER add CHANGE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пароль пользователя" (95001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'PASSWORD_MD5')) then
        execute 'alter table CORE_SRD_USER add PASSWORD_MD5 VARCHAR(32)';
    end if;
end $$;


-- Перенос колонок показателя "Электронная почта" (95001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'EMAIL')) then
        execute 'alter table CORE_SRD_USER add EMAIL VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Телефон" (95001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'PHONE')) then
        execute 'alter table CORE_SRD_USER add PHONE VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "ИД во внешней системе" (95001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER', 'EXTERNAL_ID')) then
        execute 'alter table CORE_SRD_USER add EXTERNAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (95100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGS', 'USERID')) then
        execute 'alter table CORE_SRD_USERSETTINGS add USERID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Настройки" (95100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGS', 'SETTINGS')) then
        execute 'alter table CORE_SRD_USERSETTINGS add SETTINGS VARCHAR(4000) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор связи" (95200000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER_ROLE', 'ID')) then
        execute 'alter table CORE_SRD_USER_ROLE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к пользователю" (95200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER_ROLE', 'USER_ID')) then
        execute 'alter table CORE_SRD_USER_ROLE add USER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к роли" (95200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USER_ROLE', 'ROLE_ID')) then
        execute 'alter table CORE_SRD_USER_ROLE add ROLE_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (95400100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'ID')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (95400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'USER_ID')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add USER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор раскладки" (95400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'LAYOUT_ID')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add LAYOUT_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Настройки" (95400400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_USERSETTINGSLAYOUT', 'SETTINGS')) then
        execute 'alter table CORE_SRD_USERSETTINGSLAYOUT add SETTINGS TEXT not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор фильтра" (95500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'ID')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Внешний ключ к роли" (95500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'ROLE_ID')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add ROLE_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД главного реестра в запросе, для которого должны быть применены данный фильтр" (95500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'REGISTER_ID')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД представления реестра, для которого должны быть применены данный фильтр" (95500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add REGISTER_VIEW_ID VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Фильтр в виде сериализованного QSCondition" (95500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'QSCONDITION')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add QSCONDITION TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Описание" (95500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_ROLE_FILTER', 'DESCRIPTION')) then
        execute 'alter table CORE_SRD_ROLE_FILTER add DESCRIPTION VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (95600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'ID')) then
        execute 'alter table CORE_LAYOUT_EXPORT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор раскладки" (95600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'LAYOUT_ID')) then
        execute 'alter table CORE_LAYOUT_EXPORT add LAYOUT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (95600300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'USER_ID')) then
        execute 'alter table CORE_LAYOUT_EXPORT add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала загрузки" (95600400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'START_DATE')) then
        execute 'alter table CORE_LAYOUT_EXPORT add START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата окончания загрузки" (95600500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'END_DATE')) then
        execute 'alter table CORE_LAYOUT_EXPORT add END_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус загрузки (0 - создана; 1 - запущена; 2 - завершена; 3 - ошибка)" (95600600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'STATUS')) then
        execute 'alter table CORE_LAYOUT_EXPORT add STATUS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Результирующее сообщение" (95600700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'RESULT_MESSAGE')) then
        execute 'alter table CORE_LAYOUT_EXPORT add RESULT_MESSAGE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Расположение результирующего файла" (95600800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'FILE_LOCATION')) then
        execute 'alter table CORE_LAYOUT_EXPORT add FILE_LOCATION VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Количество строк в выгрузке" (95600900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'ROWS_COUNT')) then
        execute 'alter table CORE_LAYOUT_EXPORT add ROWS_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Запрос на момент певого запуска" (95601000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'QS_QUERY')) then
        execute 'alter table CORE_LAYOUT_EXPORT add QS_QUERY TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Тип днных выгрузки" (95601100)

-- Перенос колонок показателя "Идентификатор представления реестра" (95601200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LAYOUT_EXPORT', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_LAYOUT_EXPORT add REGISTER_VIEW_ID VARCHAR(512)';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (95700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'ID')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД реестра" (95700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'REGISTER_ID')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add REGISTER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД родительской категории" (95700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'PARENT_ID')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add PARENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование категории" (95700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'NAME')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add NAME VARCHAR(256)';
    end if;
end $$;


-- Перенос колонок показателя "Фильтр" (95700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_REGISTER_CATEGORY', 'QS_CONDITION')) then
        execute 'alter table CORE_SRD_REGISTER_CATEGORY add QS_CONDITION TEXT';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (95800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION_REG_CAT', 'ID')) then
        execute 'alter table CORE_SRD_FUNCTION_REG_CAT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД функции" (95800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION_REG_CAT', 'FUNCTION_ID')) then
        execute 'alter table CORE_SRD_FUNCTION_REG_CAT add FUNCTION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД категории доступа к реестрам" (95800300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_SRD_FUNCTION_REG_CAT', 'REGISTER_CATEGORY_ID')) then
        execute 'alter table CORE_SRD_FUNCTION_REG_CAT add REGISTER_CATEGORY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор шаблона" (96000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'ID')) then
        execute 'alter table CORE_TD_TEMPLATE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор текщей версии шаблона (внешний ключ)" (96000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'CURRENT_VERSION_ID')) then
        execute 'alter table CORE_TD_TEMPLATE add CURRENT_VERSION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование шаблона" (96000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'TEMPLATE_NAME')) then
        execute 'alter table CORE_TD_TEMPLATE add TEMPLATE_NAME VARCHAR(150) not null';
    end if;
end $$;


-- Перенос колонок показателя "Нименование схемы документа (корневой узел XML данных)" (96000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE', 'SCHEME_NAME')) then
        execute 'alter table CORE_TD_TEMPLATE add SCHEME_NAME VARCHAR(50) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор (главный ключ)" (96100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'ID')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор шаблона (внешний ключ)" (96100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'TEMPLATE_ID')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add TEMPLATE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер версии шаблона" (96100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'VERSION')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add VERSION INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Шаблон (InfoPath)" (96100400)

-- Перенос колонок показателя "XSD схема данных XML документа" (96100500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'XSD')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add XSD VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "XSLT преобразование для формирования печатной формы документа на основе XML данных" (96100600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'XSL_PRINT')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add XSL_PRINT VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Путь публикации шаблона (физический путь к шаблону)" (96100700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'PUBLISH_PATH')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add PUBLISH_PATH VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (96100800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'CREATE_DATE')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add CREATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Автор шаблона (внешний ключ - имя пользователя)" (96100900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'AUTHOR')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add AUTHOR VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Полный путь к классу, представляющему модель данных шаблона ASP.NET" (96101000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'XSD_CLASS_NAME')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add XSD_CLASS_NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Тип шаблона документа (InfoPath, ASP.NET Form)" (96101100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'TEMPLATE_TYPE')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add TEMPLATE_TYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Шаблон технологического документа в формате TdForm" (96101200)

-- Перенос колонок показателя "Печатная форма технологического документа в формате DOCX с параметрами" (96101300)

-- Перенос колонок показателя "Признак, что печатная форма DOCX загружена" (96101400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_VERSION', 'PRINT_VIEW_SPECIFIED')) then
        execute 'alter table CORE_TD_TEMPLATE_VERSION add PRINT_VIEW_SPECIFIED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Номер (идентификатор) статуса" (96200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_STATUS', 'ID')) then
        execute 'alter table CORE_TD_STATUS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (96200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_STATUS', 'NAME')) then
        execute 'alter table CORE_TD_STATUS add NAME VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор (главный ключ)" (96300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'ID')) then
        execute 'alter table CORE_TD_INSTANCE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор версии шаблона (внешний ключ)" (96300200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'TEMPLATE_VERSION_ID')) then
        execute 'alter table CORE_TD_INSTANCE add TEMPLATE_VERSION_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (96300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'DESCRIPTION')) then
        execute 'alter table CORE_TD_INSTANCE add DESCRIPTION VARCHAR(150)';
    end if;
end $$;


-- Перенос колонок показателя "Автор документа (внешний ключ - имя пользователя)" (96300400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'AUTHOR_ID')) then
        execute 'alter table CORE_TD_INSTANCE add AUTHOR_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Номер" (96300500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'REGNUMBER')) then
        execute 'alter table CORE_TD_INSTANCE add REGNUMBER VARCHAR(40)';
    end if;
end $$;


-- Перенос колонок показателя "Дата" (96300600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'CREATE_DATE')) then
        execute 'alter table CORE_TD_INSTANCE add CREATE_DATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата последнего изменения" (96300700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'CHANGE_DATE')) then
        execute 'alter table CORE_TD_INSTANCE add CHANGE_DATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "XML данные документа" (96300800)

-- Перенос колонок показателя "Статус документа" (96300900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'STATUS')) then
        execute 'alter table CORE_TD_INSTANCE add STATUS INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Приоритет обработки документа службой" (96301000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'PRIORITY')) then
        execute 'alter table CORE_TD_INSTANCE add PRIORITY INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта реестра для связи с документом из ЭП" (96301100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'OBJECT_ID')) then
        execute 'alter table CORE_TD_INSTANCE add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата выпуска документа" (96301200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'APPROVE_DATE')) then
        execute 'alter table CORE_TD_INSTANCE add APPROVE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя, выпустившего документ (core_srd_user.id)" (96301300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'APPROVE_USER')) then
        execute 'alter table CORE_TD_INSTANCE add APPROVE_USER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра, в котором находится связанный объект" (96301400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_INSTANCE', 'REGISTER_ID')) then
        execute 'alter table CORE_TD_INSTANCE add REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор набора изменений (главный ключ)" (96400100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'ID')) then
        execute 'alter table CORE_TD_CHANGESET add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор технологического документа (внешний ключ)" (96400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'TD_ID')) then
        execute 'alter table CORE_TD_CHANGESET add TD_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата изменения" (96400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'CHANGESET_DATE')) then
        execute 'alter table CORE_TD_CHANGESET add CHANGESET_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус" (96400400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'STATUS')) then
        execute 'alter table CORE_TD_CHANGESET add STATUS INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (96400500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGESET', 'USER_ID')) then
        execute 'alter table CORE_TD_CHANGESET add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор изменения" (96500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'ID')) then
        execute 'alter table CORE_TD_CHANGE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор набора изменений" (96500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'CHANGESET_ID')) then
        execute 'alter table CORE_TD_CHANGE add CHANGESET_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра, в который внесены изменения" (96500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'REGISTER_ID')) then
        execute 'alter table CORE_TD_CHANGE add REGISTER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта в который внесены изменения" (96500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'OBJECT_ID')) then
        execute 'alter table CORE_TD_CHANGE add OBJECT_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор исторической записи объекта в которой внесены изменения" (96500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'QUANT_ID')) then
        execute 'alter table CORE_TD_CHANGE add QUANT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип действия с объектом" (96500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_CHANGE', 'ACTION')) then
        execute 'alter table CORE_TD_CHANGE add ACTION INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (96600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT_ACTION', 'ID')) then
        execute 'alter table CORE_TD_AUDIT_ACTION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование типа аудируемого события" (96600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT_ACTION', 'NAME')) then
        execute 'alter table CORE_TD_AUDIT_ACTION add NAME VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор действия" (96700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'ID')) then
        execute 'alter table CORE_TD_AUDIT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатолр документа" (96700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'TD_ID')) then
        execute 'alter table CORE_TD_AUDIT add TD_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор типа действия" (96700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'ACTION_ID')) then
        execute 'alter table CORE_TD_AUDIT add ACTION_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата действия" (96700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'DATE_TIME')) then
        execute 'alter table CORE_TD_AUDIT add DATE_TIME TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Результат выполнения действия" (96700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'ACTIONRESULT')) then
        execute 'alter table CORE_TD_AUDIT add ACTIONRESULT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус документа после выполнения действи" (96700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'STATUSAFTER')) then
        execute 'alter table CORE_TD_AUDIT add STATUSAFTER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Новый автор" (96700700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'NEWAUTHOR')) then
        execute 'alter table CORE_TD_AUDIT add NEWAUTHOR VARCHAR(68)';
    end if;
end $$;


-- Перенос колонок показателя "Новый регистрационный номер" (96700800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'NEWNUMBER')) then
        execute 'alter table CORE_TD_AUDIT add NEWNUMBER VARCHAR(40)';
    end if;
end $$;


-- Перенос колонок показателя "Описание действия" (96700900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'DESCRIPTION')) then
        execute 'alter table CORE_TD_AUDIT add DESCRIPTION VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (96701000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_AUDIT', 'USER_ID')) then
        execute 'alter table CORE_TD_AUDIT add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор (главный ключ)" (96800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'ID')) then
        execute 'alter table CORE_TD_TREE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор родительской папки (внешний ключ)" (96800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'PARENT_ID')) then
        execute 'alter table CORE_TD_TREE add PARENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование папки" (96800300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'FOLDER_NAME')) then
        execute 'alter table CORE_TD_TREE add FOLDER_NAME VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор шаблона" (96800400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'TEMPLATE_ID')) then
        execute 'alter table CORE_TD_TREE add TEMPLATE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Порядок следования папок и шаблонов" (96800500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TREE', 'TREE_ORDER')) then
        execute 'alter table CORE_TD_TREE add TREE_ORDER INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор образа документа" (96900100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'ID')) then
        execute 'alter table CORE_TD_ATTACHMENTS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор документа" (96900200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'TD_ID')) then
        execute 'alter table CORE_TD_ATTACHMENTS add TD_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор образа" (96900300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'ATTACHMENT_ID')) then
        execute 'alter table CORE_TD_ATTACHMENTS add ATTACHMENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак удаления" (96900400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'IS_DELETED')) then
        execute 'alter table CORE_TD_ATTACHMENTS add IS_DELETED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кем удалено" (96900500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'DELETED_BY')) then
        execute 'alter table CORE_TD_ATTACHMENTS add DELETED_BY VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Дата удаления" (96900600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_ATTACHMENTS', 'DELETED_DATE')) then
        execute 'alter table CORE_TD_ATTACHMENTS add DELETED_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (97000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TP', 'ID')) then
        execute 'alter table CORE_TD_TP add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор типа шаблона" (97100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_TYPE', 'ID')) then
        execute 'alter table CORE_TD_TEMPLATE_TYPE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип шаблона" (97100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_TD_TEMPLATE_TYPE', 'NAME')) then
        execute 'alter table CORE_TD_TEMPLATE_TYPE add NAME VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (97500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя" (97500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'USER_ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД типа долгого процесса" (97500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'PROCESS_TYPE_ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add PROCESS_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД реестра связанного объекта" (97500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'OBJECT_REGISTER_ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add OBJECT_REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД связанного объекта" (97500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'OBJECT_ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (97500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'CREATE_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add CREATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата запуска выполнения" (97500700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'START_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата завершения выполнения" (97500800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'END_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add END_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус" (97500900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'STATUS')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add STATUS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата последней проверки статуса" (97501000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'LAST_CHECK_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add LAST_CHECK_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ИД ошибки" (97501100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'ERROR_ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add ERROR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сообщение" (97501200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'MESSAGE')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add MESSAGE VARCHAR(512)';
    end if;
end $$;


-- Перенос колонок показателя "ИД журнала запуска службы" (97501300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'SERVICE_LOG_ID')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add SERVICE_LOG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Журнал (состояние процесса)" (97501400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_QUEUE', 'LOG')) then
        execute 'alter table CORE_LONG_PROCESS_QUEUE add LOG VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (97600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'ID')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Системное имя процесса" (97600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'PROCESS_NAME')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add PROCESS_NAME VARCHAR(256)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование класса" (97600300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'CLASS_NAME')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add CLASS_NAME VARCHAR(512)';
    end if;
end $$;


-- Перенос колонок показателя "Тип плана" (97600400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'SCHEDULE_TYPE')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add SCHEDULE_TYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Интервал повторения" (97600500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'REPEAT_INTERVAL')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add REPEAT_INTERVAL VARCHAR(256)';
    end if;
end $$;


-- Перенос колонок показателя "Активен" (97600600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'ENABLED')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add ENABLED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Количество запусков" (97600700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'RUN_COUNT')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add RUN_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество ошибок" (97600800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'FAILURE_COUNT')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add FAILURE_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата последнего запуска" (97600900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'LAST_START_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add LAST_START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Продолжительность последнего выполнения (в тиках)" (97601000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'LAST_RUN_DURATION')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add LAST_RUN_DURATION INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата следующего запуска" (97601100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'NEXT_RUN_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add NEXT_RUN_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Параметры" (97601200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'PARAMETERS')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add PARAMETERS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Описание" (97601300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'DESCRIPTION')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add DESCRIPTION VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Результат теста процесса" (97601400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_TYPE', 'TEST_RESULT')) then
        execute 'alter table CORE_LONG_PROCESS_TYPE add TEST_RESULT INT2';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (97700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'ID')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Параметры исполняемого файла" (97700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'EXE_INFO')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add EXE_INFO VARCHAR(1024)';
    end if;
end $$;


-- Перенос колонок показателя "Дата запуска" (97700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'START_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата последней проверки состояния" (97700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'LAST_CHECK_DATE')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add LAST_CHECK_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус" (97700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'STATUS')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add STATUS INT8';
    end if;
end $$;


-- Перенос колонок показателя "ИД пользователя" (97700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_LONG_PROCESS_LOG', 'USER_ID')) then
        execute 'alter table CORE_LONG_PROCESS_LOG add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (97800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'ID')) then
        execute 'alter table CORE_CONFIGPARAM add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ключ конфигурации" (97800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'PARENTKEY')) then
        execute 'alter table CORE_CONFIGPARAM add PARENTKEY VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Дочерний ключ конфигурации" (97800300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'CHILDKEY')) then
        execute 'alter table CORE_CONFIGPARAM add CHILDKEY VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "XML-конфигурация" (97800400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'XMLDATA')) then
        execute 'alter table CORE_CONFIGPARAM add XMLDATA TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Описание" (97800500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'DESCRIPTION')) then
        execute 'alter table CORE_CONFIGPARAM add DESCRIPTION VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Дата изменения" (97800600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'CHDATE')) then
        execute 'alter table CORE_CONFIGPARAM add CHDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Имя пользователя" (97800700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CONFIGPARAM', 'USERID')) then
        execute 'alter table CORE_CONFIGPARAM add USERID VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (98200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'REFERENCEID')) then
        execute 'alter table CORE_REFERENCE add REFERENCEID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование справочника" (98200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'DESCRIPTION')) then
        execute 'alter table CORE_REFERENCE add DESCRIPTION VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Тип данных справочных значений" (98200300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'VIDDOC')) then
        execute 'alter table CORE_REFERENCE add VIDDOC INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что справочник доступен только для чтения" (98200400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'READONLY')) then
        execute 'alter table CORE_REFERENCE add READONLY INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Полныое имя класса, реализующего доступ к данным справочника" (98200500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'PROGID')) then
        execute 'alter table CORE_REFERENCE add PROGID VARCHAR(60)';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что справочник является древовидным" (98200600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'ISTREE')) then
        execute 'alter table CORE_REFERENCE add ISTREE INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что справочник долженн отображаться в иерархическом контроле" (98200700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'DISPLAYTREE')) then
        execute 'alter table CORE_REFERENCE add DISPLAYTREE INT2';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что справочник использует таблицу treehelper" (98200800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'USETREEHELPER')) then
        execute 'alter table CORE_REFERENCE add USETREEHELPER INT2';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что справочник является табличным" (98200900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'ISTABLE')) then
        execute 'alter table CORE_REFERENCE add ISTABLE INT2';
    end if;
end $$;


-- Перенос колонок показателя "Высота контрола, который используется для получения справочных данных" (98201000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'CONTROLHEIGHT')) then
        execute 'alter table CORE_REFERENCE add CONTROLHEIGHT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ширина контрола, который используется для получения справочных данных" (98201100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'CONTROLWIDTH')) then
        execute 'alter table CORE_REFERENCE add CONTROLWIDTH INT8';
    end if;
end $$;


-- Перенос колонок показателя "Полное имя класса контрола, который используется для получения справочных данных" (98201200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'CONTROLTYPE')) then
        execute 'alter table CORE_REFERENCE add CONTROLTYPE VARCHAR(60)';
    end if;
end $$;


-- Перенос колонок показателя "HIDEREFERENCE" (98201300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'HIDEREFERENCE')) then
        execute 'alter table CORE_REFERENCE add HIDEREFERENCE INT8';
    end if;
end $$;


-- Перенос колонок показателя "SKIPTREELEVEL" (98201400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'SKIPTREELEVEL')) then
        execute 'alter table CORE_REFERENCE add SKIPTREELEVEL INT8';
    end if;
end $$;


-- Перенос колонок показателя "CUSTOMEDITOR" (98201500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'CUSTOMEDITOR')) then
        execute 'alter table CORE_REFERENCE add CUSTOMEDITOR VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор значения по умолчанию" (98201600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'DEFAULTVALUE')) then
        execute 'alter table CORE_REFERENCE add DEFAULTVALUE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Название" (98201700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE', 'NAME')) then
        execute 'alter table CORE_REFERENCE add NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (98300100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'ITEMID')) then
        execute 'alter table CORE_REFERENCE_ITEM add ITEMID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор справочника" (98300200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'REFERENCEID')) then
        execute 'alter table CORE_REFERENCE_ITEM add REFERENCEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код" (98300300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'CODE')) then
        execute 'alter table CORE_REFERENCE_ITEM add CODE VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Наименоание" (98300400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'VALUE')) then
        execute 'alter table CORE_REFERENCE_ITEM add VALUE VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Краткое наименование" (98300500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'SHORT_TITLE')) then
        execute 'alter table CORE_REFERENCE_ITEM add SHORT_TITLE VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Признак архива (удаления)" (98300600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'IS_ARCHIVES')) then
        execute 'alter table CORE_REFERENCE_ITEM add IS_ARCHIVES INT2';
    end if;
end $$;


-- Перенос колонок показателя "Имя пользователя - автора корректировок" (98300700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'USER_NAME')) then
        execute 'alter table CORE_REFERENCE_ITEM add USER_NAME VARCHAR(150)';
    end if;
end $$;


-- Перенос колонок показателя "Дата последней корректировки" (98300800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'DATE_END_CHANGE')) then
        execute 'alter table CORE_REFERENCE_ITEM add DATE_END_CHANGE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (98300900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'DATE_S')) then
        execute 'alter table CORE_REFERENCE_ITEM add DATE_S TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дополнительный флаг для фильтрации справочных значений" (98301000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'FLAG')) then
        execute 'alter table CORE_REFERENCE_ITEM add FLAG VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Название" (98301100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_ITEM', 'NAME')) then
        execute 'alter table CORE_REFERENCE_ITEM add NAME VARCHAR(128)';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ таблицы" (98400100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'RELID')) then
        execute 'alter table CORE_REFERENCE_RELATION add RELID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Название поля родительского значения" (98400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'PARENTKEY')) then
        execute 'alter table CORE_REFERENCE_RELATION add PARENTKEY VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Название поля дочернего значения" (98400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'CHILDKEY')) then
        execute 'alter table CORE_REFERENCE_RELATION add CHILDKEY VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор родительского справочника (внешний ключ к reference)" (98400400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'PARENTREF')) then
        execute 'alter table CORE_REFERENCE_RELATION add PARENTREF INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор дочернего справочника (внешний ключ к reference)" (98400500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'CHILDREF')) then
        execute 'alter table CORE_REFERENCE_RELATION add CHILDREF INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "PARENTREQ" (98400600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'PARENTREQ')) then
        execute 'alter table CORE_REFERENCE_RELATION add PARENTREQ INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уровень иерархии" (98400700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_RELATION', 'TREELEVEL')) then
        execute 'alter table CORE_REFERENCE_RELATION add TREELEVEL INT8';
    end if;
end $$;


-- Перенос колонок показателя "ID" (98500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'ID')) then
        execute 'alter table CORE_REFERENCE_TREE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "CODE" (98500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'CODE')) then
        execute 'alter table CORE_REFERENCE_TREE add CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "PARENTID" (98500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'PARENTID')) then
        execute 'alter table CORE_REFERENCE_TREE add PARENTID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "CHILDID" (98500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'CHILDID')) then
        execute 'alter table CORE_REFERENCE_TREE add CHILDID INT8';
    end if;
end $$;


-- Перенос колонок показателя "REFERENCEID" (98500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'REFERENCEID')) then
        execute 'alter table CORE_REFERENCE_TREE add REFERENCEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "LEVEL" (98500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'LEVEL')) then
        execute 'alter table CORE_REFERENCE_TREE add LEVEL INT8';
    end if;
end $$;


-- Перенос колонок показателя "CLDREFERENCEID" (98500700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'CLDREFERENCEID')) then
        execute 'alter table CORE_REFERENCE_TREE add CLDREFERENCEID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ADRESS_TYPE" (98500800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REFERENCE_TREE', 'ADRESS_TYPE')) then
        execute 'alter table CORE_REFERENCE_TREE add ADRESS_TYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор образа" (98600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'ID')) then
        execute 'alter table CORE_ATTACHMENT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Номер" (98600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'DOC_NUMBER')) then
        execute 'alter table CORE_ATTACHMENT add DOC_NUMBER VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Описание" (98600300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'DESCRIPTION')) then
        execute 'alter table CORE_ATTACHMENT add DESCRIPTION VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Штрих-код" (98600400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'BARCODE')) then
        execute 'alter table CORE_ATTACHMENT add BARCODE VARCHAR(32)';
    end if;
end $$;


-- Перенос колонок показателя "Тип" (98600500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'DOC_TYPE')) then
        execute 'alter table CORE_ATTACHMENT add DOC_TYPE VARCHAR(120)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('CORE_ATTACHMENT', 'DOC_TYPE_ID')) then
        execute 'alter table CORE_ATTACHMENT add DOC_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип фотографии" (98600600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'PHOTO_TYPE')) then
        execute 'alter table CORE_ATTACHMENT add PHOTO_TYPE VARCHAR(120)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('CORE_ATTACHMENT', 'PHOTO_TYPE_ID')) then
        execute 'alter table CORE_ATTACHMENT add PHOTO_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя, создавшего образ" (98600700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'CREATED_BY_ID')) then
        execute 'alter table CORE_ATTACHMENT add CREATED_BY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (98600800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'CREATED_DATE')) then
        execute 'alter table CORE_ATTACHMENT add CREATED_DATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что образ удален" (98600900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'IS_DELETED')) then
        execute 'alter table CORE_ATTACHMENT add IS_DELETED INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя, удалившего образ" (98601000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'DELETED_BY_ID')) then
        execute 'alter table CORE_ATTACHMENT add DELETED_BY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата удаления образа" (98601100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT', 'DELETED_DATE')) then
        execute 'alter table CORE_ATTACHMENT add DELETED_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор файла" (98700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'ID')) then
        execute 'alter table CORE_ATTACHMENT_FILE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор образа" (98700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'ATTACHMENT_ID')) then
        execute 'alter table CORE_ATTACHMENT_FILE add ATTACHMENT_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Данные файла" (98700300)

-- Перенос колонок показателя "Имя файла" (98700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'FILENAME')) then
        execute 'alter table CORE_ATTACHMENT_FILE add FILENAME VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Тип файла" (98700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'MIMETYPE')) then
        execute 'alter table CORE_ATTACHMENT_FILE add MIMETYPE VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Номер страницы образа" (98700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'PAGE')) then
        execute 'alter table CORE_ATTACHMENT_FILE add PAGE INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак основного файла образа" (98700700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_FILE', 'IS_MAIN')) then
        execute 'alter table CORE_ATTACHMENT_FILE add IS_MAIN INT2';
    end if;
end $$;


-- Перенос колонок показателя "Миниатюра фотографии" (98700800)

-- Перенос колонок показателя "Идентификатор связи образа с объектом реестра" (98800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'ID')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор образа" (98800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'ATTACHMENT_ID')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add ATTACHMENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор реестра объекта" (98800300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'REGISTER_ID')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add REGISTER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта" (98800400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'OBJECT_ID')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что связь удалена" (98800500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'IS_DELETED')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add IS_DELETED INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя, удалившего связь" (98800600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'DELETED_BY_ID')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add DELETED_BY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата удаления связи" (98800700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'DELETED_DATE')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add DELETED_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Признак основного образа объекта" (98800800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'IS_MAIN')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add IS_MAIN INT2';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя, создавшего связь" (98800900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'CREATED_BY_ID')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add CREATED_BY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания связи" (98801000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ATTACHMENT_OBJECT', 'CREATED_DATE')) then
        execute 'alter table CORE_ATTACHMENT_OBJECT add CREATED_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ID ошибки" (98900100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'ID')) then
        execute 'alter table CORE_ERROR_LOG add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ID пользователя" (98900200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'USERID')) then
        execute 'alter table CORE_ERROR_LOG add USERID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Описание ошибки" (98900400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'MESSAGE')) then
        execute 'alter table CORE_ERROR_LOG add MESSAGE VARCHAR(2000)';
    end if;
end $$;


-- Перенос колонок показателя "Дата ошибки" (98900600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'ERRORDATE')) then
        execute 'alter table CORE_ERROR_LOG add ERRORDATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что пользователю была показана стандартная страница с ошибкой" (98900700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'ERRORPAGE_SHOWN')) then
        execute 'alter table CORE_ERROR_LOG add ERRORPAGE_SHOWN INT2 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип сообщения" (98900800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'MSGTYPE')) then
        execute 'alter table CORE_ERROR_LOG add MSGTYPE VARCHAR(10)';
    end if;
end $$;


-- Перенос колонок показателя "Параметры (до 4000 символов)" (98900900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'PARAMS_SHORT')) then
        execute 'alter table CORE_ERROR_LOG add PARAMS_SHORT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Параметры (более 4000 символов)" (98901000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'PARAMS_CLOB')) then
        execute 'alter table CORE_ERROR_LOG add PARAMS_CLOB TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Стек вызовов (до 4000 символов)" (98901100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'CALLSTACK')) then
        execute 'alter table CORE_ERROR_LOG add CALLSTACK VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Стек вызовов (более 4000 символов)" (98901200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_ERROR_LOG', 'CALLSTACK_CLOB')) then
        execute 'alter table CORE_ERROR_LOG add CALLSTACK_CLOB TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта (главный ключ)" (99100100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'ID')) then
        execute 'alter table CORE_REGISTER_STATE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Название представления" (99100200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'REGISTER_VIEW_ID')) then
        execute 'alter table CORE_REGISTER_STATE add REGISTER_VIEW_ID VARCHAR(100) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (99100300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'USER_ID')) then
        execute 'alter table CORE_REGISTER_STATE add USER_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата сохраненения состояния" (99100400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGISTER_STATE', 'STATE_SAVE_DATE')) then
        execute 'alter table CORE_REGISTER_STATE add STATE_SAVE_DATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Сериализованное состояние представления" (99100500)

-- Перенос колонок показателя "Идентификатор записи" (99200100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'ID')) then
        execute 'alter table CORE_DIAGNOSTICS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя, которым было совершено данное действие" (99200200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'USER_ID')) then
        execute 'alter table CORE_DIAGNOSTICS add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование подсистемы (модуля), в которой было совершено данное действие" (99200300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'MODULE')) then
        execute 'alter table CORE_DIAGNOSTICS add MODULE VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование метода в подсистеме (модуле), в которой было совершено данное действие" (99200400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'METHOD')) then
        execute 'alter table CORE_DIAGNOSTICS add METHOD VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Дополнительные ключевые параметры метода" (99200500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'EXTRA_KEY')) then
        execute 'alter table CORE_DIAGNOSTICS add EXTRA_KEY VARCHAR(250)';
    end if;
end $$;


-- Перенос колонок показателя "Дата и время совершения действия" (99200600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'ACTION_DATE')) then
        execute 'alter table CORE_DIAGNOSTICS add ACTION_DATE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Длительность выполнения действия в Tiсks" (99200700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'EXECUTION_DURATION')) then
        execute 'alter table CORE_DIAGNOSTICS add EXECUTION_DURATION INT8';
    end if;
end $$;


-- Перенос колонок показателя "Описание совершенного действия" (99200800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'ACTION_DESCR')) then
        execute 'alter table CORE_DIAGNOSTICS add ACTION_DESCR TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Стек вызовов (до 4000 символов)" (99201000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'CALLSTACK')) then
        execute 'alter table CORE_DIAGNOSTICS add CALLSTACK VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Стек вызовов (более 4000 символов)" (99201100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_DIAGNOSTICS', 'CALLSTACK_CLOB')) then
        execute 'alter table CORE_DIAGNOSTICS add CALLSTACK_CLOB TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (99400100)

-- Перенос колонок показателя "Сформированный регистрационный номер" (99400200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'REGNOMVALUE')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add REGNOMVALUE VARCHAR(120)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор последовательности" (99400300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'IDSEQUENCE')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add IDSEQUENCE INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор инкремента" (99400400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_REPOSITORY', 'REGNOMINCREMENT')) then
        execute 'alter table CORE_REGNOM_REPOSITORY add REGNOMINCREMENT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (99500100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'ID')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор нумератора" (99500200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'NUMERATORID')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add NUMERATORID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип объекта" (99500300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'REGNOMTYPE')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add REGNOMTYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 1" (99500400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR1')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR1 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 0" (99500500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR0')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR0 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 2" (99500600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR2')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR2 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 3" (99500700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR3')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR3 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 4" (99500800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR4')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR4 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 5" (99500900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR5')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR5 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 6" (99501000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR6')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR6 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 7" (99501100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR7')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR7 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 8" (99501200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR8')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR8 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Параметр последовательности 9" (99501300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'PAR9')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add PAR9 VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Текущее значение последовательности" (99501400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_REGNOM_SEQUENCES', 'CURRENTINCREMENT')) then
        execute 'alter table CORE_REGNOM_SEQUENCES add CURRENTINCREMENT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (99600100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'ID')) then
        execute 'alter table CORE_CACHE_UPDATES add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Имя менеджера кэша" (99600200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'CACHEOBJECT')) then
        execute 'alter table CORE_CACHE_UPDATES add CACHEOBJECT VARCHAR(50) not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта кэширования внутри менеджера кэша" (99600300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'CACHEKEY')) then
        execute 'alter table CORE_CACHE_UPDATES add CACHEKEY INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дополнительный идентификатор объекта кэширования внутри менеджера кэша" (99600400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'EXTRADATA')) then
        execute 'alter table CORE_CACHE_UPDATES add EXTRADATA VARCHAR(200) not null';
    end if;
end $$;


-- Перенос колонок показателя "Время последнего обновления объекта реестра" (99600500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_CACHE_UPDATES', 'CACHE_TIMESTAMP')) then
        execute 'alter table CORE_CACHE_UPDATES add CACHE_TIMESTAMP TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Главный ключ" (99700100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'ID')) then
        execute 'alter table CORE_UPDSTRU_LOG add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата и время начала выполнения скрипта" (99700200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'DATE_START')) then
        execute 'alter table CORE_UPDSTRU_LOG add DATE_START TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата и время завершения выполнения скрипта" (99700300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'DATE_FINISH')) then
        execute 'alter table CORE_UPDSTRU_LOG add DATE_FINISH TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Название скрипта" (99700400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'SCRIPT_NAME')) then
        execute 'alter table CORE_UPDSTRU_LOG add SCRIPT_NAME VARCHAR(100) not null';
    end if;
end $$;


-- Перенос колонок показателя "Версия скрипта" (99700500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'SCRIPT_VERSION')) then
        execute 'alter table CORE_UPDSTRU_LOG add SCRIPT_VERSION VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Признак, что при выполнении скрипт содержал ошибку" (99700600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'HAS_ERR')) then
        execute 'alter table CORE_UPDSTRU_LOG add HAS_ERR INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дополнительное сообщение. Или подробное сообщение об ошибке." (99700700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_UPDSTRU_LOG', 'RESULT_MESSAGE')) then
        execute 'alter table CORE_UPDSTRU_LOG add RESULT_MESSAGE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (99800100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_HOLIDAYS', 'ID')) then
        execute 'alter table CORE_HOLIDAYS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата выходного или праздничного дня" (99800200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('CORE_HOLIDAYS', 'VALUE')) then
        execute 'alter table CORE_HOLIDAYS add VALUE TIMESTAMP not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (258000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'OKRUG_ID')) then
        execute 'alter table REF_ADDR_OKRUG add OKRUG_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Полное наименование округа" (258000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'FULL_NAME')) then
        execute 'alter table REF_ADDR_OKRUG add FULL_NAME VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Сокращенное наименование округа" (258000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'SHORT_NAME')) then
        execute 'alter table REF_ADDR_OKRUG add SHORT_NAME VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Код округа" (258000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'STEKS_CODE')) then
        execute 'alter table REF_ADDR_OKRUG add STEKS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на субъект" (258000500)

-- Перенос колонок показателя "Наименование для сортировки" (258000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'NAME_FOR_SORT')) then
        execute 'alter table REF_ADDR_OKRUG add NAME_FOR_SORT VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Код ОМК" (258000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'OMK_CODE')) then
        execute 'alter table REF_ADDR_OKRUG add OMK_CODE VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Тип" (258000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'TYPE_REF')) then
        execute 'alter table REF_ADDR_OKRUG add TYPE_REF INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код округа GIVC" (258000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'CODE_GIVC')) then
        execute 'alter table REF_ADDR_OKRUG add CODE_GIVC VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор страховой компании" (258001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_OKRUG', 'INSURANCE_COMPANY_ID')) then
        execute 'alter table REF_ADDR_OKRUG add INSURANCE_COMPANY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (259000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'DISTRICT_ID')) then
        execute 'alter table REF_ADDR_DISTRICT add DISTRICT_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Полное наименование района" (259000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'FULL_NAME')) then
        execute 'alter table REF_ADDR_DISTRICT add FULL_NAME VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Краткое наименование района" (259000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'SHORT_NAME')) then
        execute 'alter table REF_ADDR_DISTRICT add SHORT_NAME VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Код района" (259000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'STEKS_CODE')) then
        execute 'alter table REF_ADDR_DISTRICT add STEKS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор округа" (259000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'OKRUG_ID')) then
        execute 'alter table REF_ADDR_DISTRICT add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на субъект" (259000600)

-- Перенос колонок показателя "Наименование для сортировки" (259000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'NAME_FOR_SORT')) then
        execute 'alter table REF_ADDR_DISTRICT add NAME_FOR_SORT VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Код ОМК" (259000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'OMK_CODE')) then
        execute 'alter table REF_ADDR_DISTRICT add OMK_CODE VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Тип" (259000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'TYPE_REF')) then
        execute 'alter table REF_ADDR_DISTRICT add TYPE_REF INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код округа GIVC" (259001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('REF_ADDR_DISTRICT', 'CODE_GIVC')) then
        execute 'alter table REF_ADDR_DISTRICT add CODE_GIVC VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (301000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'EMP_ID')) then
        execute 'alter table INSUR_INPUT_FILE add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Название файла" (301000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'FILE_NAME')) then
        execute 'alter table INSUR_INPUT_FILE add FILE_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код типа файла  на основании справочника «Код типа файла»" (301000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'TYPE_FILE')) then
        execute 'alter table INSUR_INPUT_FILE add TYPE_FILE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'TYPE_FILE_CODE')) then
        execute 'alter table INSUR_INPUT_FILE add TYPE_FILE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Период учета данных в Системе" (301000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_INPUT_FILE add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор района" (301000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'DISTRICT_ID')) then
        execute 'alter table INSUR_INPUT_FILE add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Источник  (1-МФЦ, 2-СК)" (301000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'TYPE_SOURCE')) then
        execute 'alter table INSUR_INPUT_FILE add TYPE_SOURCE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'TYPE_SOURCE_CODE')) then
        execute 'alter table INSUR_INPUT_FILE add TYPE_SOURCE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки в Систему" (301000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'DATE_INPUT')) then
        execute 'alter table INSUR_INPUT_FILE add DATE_INPUT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Количество строк в файле" (301000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'COUNT_STR')) then
        execute 'alter table INSUR_INPUT_FILE add COUNT_STR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус загрузки/обработки файла" (301000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'STATUS')) then
        execute 'alter table INSUR_INPUT_FILE add STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'STATUS_CODE')) then
        execute 'alter table INSUR_INPUT_FILE add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пакета загрузки" (301001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'LINK_PACKAGE')) then
        execute 'alter table INSUR_INPUT_FILE add LINK_PACKAGE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на пользователя" (301001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'USER_ID')) then
        execute 'alter table INSUR_INPUT_FILE add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общая сумма по данным файла" (301001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'SUM_ALL')) then
        execute 'alter table INSUR_INPUT_FILE add SUM_ALL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на OMFileStorage" (301001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'FILE_STORAGE_ID')) then
        execute 'alter table INSUR_INPUT_FILE add FILE_STORAGE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Пройдена процедура установки критериев" (301001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'CRITERIA_SET')) then
        execute 'alter table INSUR_INPUT_FILE add CRITERIA_SET INT2';
    end if;
end $$;


-- Перенос колонок показателя "Код поставщика" (301001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'KOD_POST')) then
        execute 'alter table INSUR_INPUT_FILE add KOD_POST INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на ранее загруженный файл" (301001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'PARENT_ID')) then
        execute 'alter table INSUR_INPUT_FILE add PARENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Загружено строк" (301001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_FILE', 'COUNT_STR_LOAD')) then
        execute 'alter table INSUR_INPUT_FILE add COUNT_STR_LOAD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (302000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'EMP_ID')) then
        execute 'alter table INSUR_LOG_FILE add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на загружаемый файл" (302000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'FILE_STORAGE_ID')) then
        execute 'alter table INSUR_LOG_FILE add FILE_STORAGE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (302000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'LOADDATE')) then
        execute 'alter table INSUR_LOG_FILE add LOADDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Результаты загрузки пакет данных (LOG-файл)" (302000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'TRACEDATA')) then
        execute 'alter table INSUR_LOG_FILE add TRACEDATA TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор округа" (302000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'OKRUG_ID')) then
        execute 'alter table INSUR_LOG_FILE add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Период" (302000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_LOG_FILE add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Прогресс загрузки" (302000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'STATUS')) then
        execute 'alter table INSUR_LOG_FILE add STATUS VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_LOG_FILE', 'STATUS_CODE')) then
        execute 'alter table INSUR_LOG_FILE add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общий статус загрузки" (302000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'GENERAL_STATUS')) then
        execute 'alter table INSUR_LOG_FILE add GENERAL_STATUS VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_LOG_FILE', 'GENERAL_STATUS_CODE')) then
        execute 'alter table INSUR_LOG_FILE add GENERAL_STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала загрузки" (302000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'START_DATE')) then
        execute 'alter table INSUR_LOG_FILE add START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата окончания загрузки" (302001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LOG_FILE', 'END_DATE')) then
        execute 'alter table INSUR_LOG_FILE add END_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (303000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'EMP_ID')) then
        execute 'alter table INSUR_BANK_PLAT add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_INPUT_FILE" (303000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_BANK_PLAT add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_SVOD_BANK" (303000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'LINK_SVOD_BANK')) then
        execute 'alter table INSUR_BANK_PLAT add LINK_SVOD_BANK INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор района" (303000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'DISTRICT_ID')) then
        execute 'alter table INSUR_BANK_PLAT add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Банковский день" (303000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'BANK_DAY')) then
        execute 'alter table INSUR_BANK_PLAT add BANK_DAY TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код плательщика" (303000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KODPL')) then
        execute 'alter table INSUR_BANK_PLAT add KODPL VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Первое число месяца, в котором данные должны быть учтены на ФСП" (303000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_BANK_PLAT add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Оплачиваемый период" (303000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'PERIOD')) then
        execute 'alter table INSUR_BANK_PLAT add PERIOD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер документа" (303000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'NOM_DOC')) then
        execute 'alter table INSUR_BANK_PLAT add NOM_DOC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Cумма платежа (всего по ЕПД)" (303001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'SUM_ALL')) then
        execute 'alter table INSUR_BANK_PLAT add SUM_ALL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Комиссия Банка" (303001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOM_BANK_ALL')) then
        execute 'alter table INSUR_BANK_PLAT add KOM_BANK_ALL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "БИК Банка" (303001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'BIC_BANK')) then
        execute 'alter table INSUR_BANK_PLAT add BIC_BANK INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата платежа" (303001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'DATA_PP')) then
        execute 'alter table INSUR_BANK_PLAT add DATA_PP TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный код документа" (303001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'COD_DOC')) then
        execute 'alter table INSUR_BANK_PLAT add COD_DOC INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код услуги" (303001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOD_YSL')) then
        execute 'alter table INSUR_BANK_PLAT add KOD_YSL INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код поставщика" (303001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOD_POST')) then
        execute 'alter table INSUR_BANK_PLAT add KOD_POST INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сумма платежа по коду" (303001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'SUM_BY_CODE')) then
        execute 'alter table INSUR_BANK_PLAT add SUM_BY_CODE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Комиссия Банка" (303001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOM_BANK')) then
        execute 'alter table INSUR_BANK_PLAT add KOM_BANK NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Комиссия за обработку" (303001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOM_BANK_OBR')) then
        execute 'alter table INSUR_BANK_PLAT add KOM_BANK_OBR NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Комиссия ЕИРЦ" (303002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOM_EIRC')) then
        execute 'alter table INSUR_BANK_PLAT add KOM_EIRC NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Комиссия за работу с плательщиками" (303002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOM_PLAT')) then
        execute 'alter table INSUR_BANK_PLAT add KOM_PLAT NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Период документа" (303002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'DOC_PERIOD')) then
        execute 'alter table INSUR_BANK_PLAT add DOC_PERIOD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Признак распределения" (303002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'FLAG_VOZVR')) then
        execute 'alter table INSUR_BANK_PLAT add FLAG_VOZVR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип оплаты" (303002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'TYPE_OPL')) then
        execute 'alter table INSUR_BANK_PLAT add TYPE_OPL INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код управляющей компании" (303002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'KOD_YPRAVL')) then
        execute 'alter table INSUR_BANK_PLAT add KOD_YPRAVL INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак начисления" (303002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'FLAG_NACH')) then
        execute 'alter table INSUR_BANK_PLAT add FLAG_NACH INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сумма платежей в файле" (303002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'SUM_VSEGO')) then
        execute 'alter table INSUR_BANK_PLAT add SUM_VSEGO NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во строк в файле" (303002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK_PLAT', 'STROK_VSEGO')) then
        execute 'alter table INSUR_BANK_PLAT add STROK_VSEGO NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (304000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'EMP_ID')) then
        execute 'alter table INSUR_SVOD_BANK add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE" (304000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_SVOD_BANK add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Название файла" (304000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'FILE_NAME')) then
        execute 'alter table INSUR_SVOD_BANK add FILE_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Банковский день" (304000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'BANK_DAY')) then
        execute 'alter table INSUR_SVOD_BANK add BANK_DAY TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор района" (304000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'DISTRICT_ID')) then
        execute 'alter table INSUR_SVOD_BANK add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во строк в файле" (304000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'STR')) then
        execute 'alter table INSUR_SVOD_BANK add STR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общая сумма платежей" (304000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'PAY_SUM')) then
        execute 'alter table INSUR_SVOD_BANK add PAY_SUM NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Код поставщика" (304000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SVOD_BANK', 'COD_POST')) then
        execute 'alter table INSUR_SVOD_BANK add COD_POST VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (305000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'EMP_ID')) then
        execute 'alter table INSUR_INPUT_NACH add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE" (305000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_INPUT_NACH add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр ФСП INSUR_FSP" (305000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'FSP_ID')) then
        execute 'alter table INSUR_INPUT_NACH add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Источник  (1-МФЦ, 2-СК, 4-ГБУ)" (305000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'TYPE_SOURCE')) then
        execute 'alter table INSUR_INPUT_NACH add TYPE_SOURCE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'TYPE_SOURCE_CODE')) then
        execute 'alter table INSUR_INPUT_NACH add TYPE_SOURCE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус идентификации записи" (305000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'STATUS_IDENTIF')) then
        execute 'alter table INSUR_INPUT_NACH add STATUS_IDENTIF VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'STATUS_IDENTIF_CODE')) then
        execute 'alter table INSUR_INPUT_NACH add STATUS_IDENTIF_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Период учета данных в Системе" (305000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_INPUT_NACH add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Период, за который произведена оплата" (305000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'PERIOD')) then
        execute 'alter table INSUR_INPUT_NACH add PERIOD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор района" (305000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'DISTRICT_ID')) then
        execute 'alter table INSUR_INPUT_NACH add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код страховой организации " (305001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'KOD')) then
        execute 'alter table INSUR_INPUT_NACH add KOD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер дома" (305001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'UNOM')) then
        execute 'alter table INSUR_INPUT_NACH add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Адрес дома " (305001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'ADRES_T')) then
        execute 'alter table INSUR_INPUT_NACH add ADRES_T VARCHAR(2000)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер квартиры в доме" (305001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'UNKVA')) then
        execute 'alter table INSUR_INPUT_NACH add UNKVA VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Индекс квартиры" (305001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'NOMI')) then
        execute 'alter table INSUR_INPUT_NACH add NOMI VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры" (305001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'NOM')) then
        execute 'alter table INSUR_INPUT_NACH add NOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры" (305001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'KVNOM')) then
        execute 'alter table INSUR_INPUT_NACH add KVNOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на статус жилого помещения" (305001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'FLAT_STATUS_ID')) then
        execute 'alter table INSUR_INPUT_NACH add FLAT_STATUS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на тип жилого помещения" (305001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'FLAT_TYPE_ID')) then
        execute 'alter table INSUR_INPUT_NACH add FLAT_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество жилых помещений в квартире" (305002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'KOLGP')) then
        execute 'alter table INSUR_INPUT_NACH add KOLGP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общая площадь квартиры" (305002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'FOPL')) then
        execute 'alter table INSUR_INPUT_NACH add FOPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Подлежащая страхованию площадь жилого помещения" (305002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'OPL')) then
        execute 'alter table INSUR_INPUT_NACH add OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Код плательщика" (305002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'KODPL')) then
        execute 'alter table INSUR_INPUT_NACH add KODPL VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Лицевой счет" (305002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'LS')) then
        execute 'alter table INSUR_INPUT_NACH add LS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Величина начисленного страхового взноса" (305002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'SUM_NACH')) then
        execute 'alter table INSUR_INPUT_NACH add SUM_NACH NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "1/0 (UNOM найден в Адресном списке/ UNOM не найден в Адресном списке)" (305002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'FLAG_UNOM_NO')) then
        execute 'alter table INSUR_INPUT_NACH add FLAG_UNOM_NO INT8';
    end if;
end $$;


-- Перенос колонок показателя "ФИО плательщика" (305002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'FIO')) then
        execute 'alter table INSUR_INPUT_NACH add FIO VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Статус загрузки" (305002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'LOAD_STATUS')) then
        execute 'alter table INSUR_INPUT_NACH add LOAD_STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'LOAD_STATUS_CODE')) then
        execute 'alter table INSUR_INPUT_NACH add LOAD_STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Список вхождения по критериям" (305002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_NACH', 'CRITERIA_JSON')) then
        execute 'alter table INSUR_INPUT_NACH add CRITERIA_JSON TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (306000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'EMP_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE" (306000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_INPUT_PLAT add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр ФСП INSUR_FSP" (306000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'FSP_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр c банковскими днями INSUR_BANK_PLAT" (306000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'LINK_BANK_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add LINK_BANK_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер дома" (306000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'UNOM')) then
        execute 'alter table INSUR_INPUT_PLAT add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Адрес дома" (306000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'ADRES')) then
        execute 'alter table INSUR_INPUT_PLAT add ADRES VARCHAR(2000)';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры" (306000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'NOM')) then
        execute 'alter table INSUR_INPUT_PLAT add NOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код плательщика" (306000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'KODPL')) then
        execute 'alter table INSUR_INPUT_PLAT add KODPL VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Лицевой счет" (306000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'LS')) then
        execute 'alter table INSUR_INPUT_PLAT add LS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Банковская кодировка" (306001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'TX_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add TX_ID VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата оплаты" (306001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'PMT_DATE')) then
        execute 'alter table INSUR_INPUT_PLAT add PMT_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата поступления оплаты в банк" (306001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'DATE_IN_TOFK')) then
        execute 'alter table INSUR_INPUT_PLAT add DATE_IN_TOFK TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Период, за который произведена оплата" (306001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'PERIOD')) then
        execute 'alter table INSUR_INPUT_PLAT add PERIOD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Период учета данных в Системе" (306001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_INPUT_PLAT add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Начисленная сумма" (306001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'SUM_NACH')) then
        execute 'alter table INSUR_INPUT_PLAT add SUM_NACH NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Оплаченная сумма (может быть отрицательным числом)" (306001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'SUM_OPL')) then
        execute 'alter table INSUR_INPUT_PLAT add SUM_OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Комиссия банка" (306001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'FEE')) then
        execute 'alter table INSUR_INPUT_PLAT add FEE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь, подлежащая страхованию" (306001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'OPL')) then
        execute 'alter table INSUR_INPUT_PLAT add OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор типа жилого помещения" (306001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'FLAT_TYPE_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add FLAT_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ФИО" (306002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'FIO')) then
        execute 'alter table INSUR_INPUT_PLAT add FIO VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий" (306002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'COMMENT')) then
        execute 'alter table INSUR_INPUT_PLAT add COMMENT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Статус идентификации записи" (306002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'STATUS_IDENTIF')) then
        execute 'alter table INSUR_INPUT_PLAT add STATUS_IDENTIF VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'STATUS_IDENTIF_CODE')) then
        execute 'alter table INSUR_INPUT_PLAT add STATUS_IDENTIF_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Источник  (1-МФЦ, 2-СК, 3-ГБУ)" (306002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'TYPE_SOURCE')) then
        execute 'alter table INSUR_INPUT_PLAT add TYPE_SOURCE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'TYPE_SOURCE_CODE')) then
        execute 'alter table INSUR_INPUT_PLAT add TYPE_SOURCE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "1/0 (UNOM найден в Адресном списке/ UNOM не найден в Адресном списке)" (306002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'FLAG_UNOM_NO')) then
        execute 'alter table INSUR_INPUT_PLAT add FLAG_UNOM_NO INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип договора " (306002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'TYPE_DOC')) then
        execute 'alter table INSUR_INPUT_PLAT add TYPE_DOC VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'TYPE_DOC_CODE')) then
        execute 'alter table INSUR_INPUT_PLAT add TYPE_DOC_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код страховой организации" (306002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'KOD')) then
        execute 'alter table INSUR_INPUT_PLAT add KOD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер договора страхования (для платежей по договорам  общего имущества)" (306002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'NDOG')) then
        execute 'alter table INSUR_INPUT_PLAT add NDOG VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора(для платежей по договорам  общего имущества)" (306002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'NDOGDAT')) then
        execute 'alter table INSUR_INPUT_PLAT add NDOGDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер дополнительного соглашения((для платежей по договорам  общего имущества)" (306003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'NDOPS')) then
        execute 'alter table INSUR_INPUT_PLAT add NDOPS VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер платежного поручения по договору (для платежей по договорам  общего имущества)" (306003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'PAYNUMBER')) then
        execute 'alter table INSUR_INPUT_PLAT add PAYNUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на строку в Реестре INSUR_POLICY_SVD" (306003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'INSUR_POLICY_SVD_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add INSUR_POLICY_SVD_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор района" (306003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'DISTRICT_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус загрузки" (306003500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'LOAD_STATUS')) then
        execute 'alter table INSUR_INPUT_PLAT add LOAD_STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'LOAD_STATUS_CODE')) then
        execute 'alter table INSUR_INPUT_PLAT add LOAD_STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Список вхождения по критериям" (306003600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'CRITERIA_JSON')) then
        execute 'alter table INSUR_INPUT_PLAT add CRITERIA_JSON TEXT';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр AllProrepty" (306003700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INPUT_PLAT', 'LINK_ALL_PROPERTY_ID')) then
        execute 'alter table INSUR_INPUT_PLAT add LINK_ALL_PROPERTY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (307000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'EMP_ID')) then
        execute 'alter table INSUR_BALANCE add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на ФСП INSUR_FSP_Q.EMP_ID" (307000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'FSP_ID')) then
        execute 'alter table INSUR_BALANCE add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Период учета данных в Системе" (307000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_BALANCE add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "1/0 (Оплачено/не оплачено начисление)" (307000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'FLAG_OPL')) then
        execute 'alter table INSUR_BALANCE add FLAG_OPL INT2';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на запись по начислению в INSUR_INPUT_NACH" (307000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'LINK_INPUT_NACH')) then
        execute 'alter table INSUR_BALANCE add LINK_INPUT_NACH INT8';
    end if;
end $$;


-- Перенос колонок показателя "1/0 (Застрахован период/Не застрахован)" (307000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'FLAG_INSUR')) then
        execute 'alter table INSUR_BALANCE add FLAG_INSUR INT2';
    end if;
end $$;


-- Перенос колонок показателя "Нераспределенный остаток на начало периода" (307000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'OSTATOK_SUM')) then
        execute 'alter table INSUR_BALANCE add OSTATOK_SUM NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Сумма зачислений, нарастающим итогом" (307000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'SUM_OPL')) then
        execute 'alter table INSUR_BALANCE add SUM_OPL NUMERIC(10, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Сумма начислений МФЦ в периоде, нарастающим итогом" (307000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'SUM_NACH_MFC')) then
        execute 'alter table INSUR_BALANCE add SUM_NACH_MFC NUMERIC(10, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Сумма начислений ГБУ в периоде, нарастающим итогом" (307001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'SUM_NACH_GBY')) then
        execute 'alter table INSUR_BALANCE add SUM_NACH_GBY NUMERIC(10, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Сумма оплаченных начислений  в периоде, нарастающим итогом" (307001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BALANCE', 'SUM_NACH_OPL')) then
        execute 'alter table INSUR_BALANCE add SUM_NACH_OPL NUMERIC(10, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (308000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'EMP_ID')) then
        execute 'alter table INSUR_FSP_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип ФСП" (308000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'FSP_TYPE')) then
        execute 'alter table INSUR_FSP_Q add FSP_TYPE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FSP_Q', 'FSP_TYPE_CODE')) then
        execute 'alter table INSUR_FSP_Q add FSP_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер  ФСП " (308000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'FSP_NUMBER')) then
        execute 'alter table INSUR_FSP_Q add FSP_NUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер лицевого счета" (308000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'LS')) then
        execute 'alter table INSUR_FSP_Q add LS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта " (308000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'OBJ_ID')) then
        execute 'alter table INSUR_FSP_Q add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра объекта " (308000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'OBJ_REESTR_ID')) then
        execute 'alter table INSUR_FSP_Q add OBJ_REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор договора ( полиса/свидетельства/договора страхования общего имущества)" (308000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'CONTRACT_ID')) then
        execute 'alter table INSUR_FSP_Q add CONTRACT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра договоров" (308000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'ID_REESTR_CONTR')) then
        execute 'alter table INSUR_FSP_Q add ID_REESTR_CONTR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания ФСП" (308001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'DATE_OPEN')) then
        execute 'alter table INSUR_FSP_Q add DATE_OPEN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код плательщика" (308001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'KODPL')) then
        execute 'alter table INSUR_FSP_Q add KODPL VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Площадь, подлежащая страхованию " (308001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FSP_Q', 'OPL_KODPL')) then
        execute 'alter table INSUR_FSP_Q add OPL_KODPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (309000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'EMP_ID')) then
        execute 'alter table INSUR_POLICY_SVD add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип документа" (309000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'TYPE_DOC')) then
        execute 'alter table INSUR_POLICY_SVD add TYPE_DOC VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'TYPE_DOC_CODE')) then
        execute 'alter table INSUR_POLICY_SVD add TYPE_DOC_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на ФСП INSUR_FSP_Q.EMP_ID" (309000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'FSP_ID')) then
        execute 'alter table INSUR_POLICY_SVD add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл загрузки, строка в Реестре INSUR_INPUT_FILE" (309000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_POLICY_SVD add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл загрузки, используется при загрузке данных из файла POLYC_D.DBF" (309000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'LINK_ID_FILE_END')) then
        execute 'alter table INSUR_POLICY_SVD add LINK_ID_FILE_END INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на страховую организацию" (309000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'INSURANCE_ORGANIZATION_ID')) then
        execute 'alter table INSUR_POLICY_SVD add INSURANCE_ORGANIZATION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ID округа " (309000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'OKRUG_ID')) then
        execute 'alter table INSUR_POLICY_SVD add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ID района " (309000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'DISTRICT_ID')) then
        execute 'alter table INSUR_POLICY_SVD add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код формы объединения собственников (6 – ЖСК, ЖК,  7 – ТСЖ, 8 – БО (без объединения собственников), 0 – нет данных)" (309000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'ORG_TYPE')) then
        execute 'alter table INSUR_POLICY_SVD add ORG_TYPE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код управляющей организации (УО)" (309001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'ORG_ID')) then
        execute 'alter table INSUR_POLICY_SVD add ORG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код вида организации, начисляющей страховые взносы – по справочнику «Виды организаций, начисляющих страховые взносы»" (309001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'PLAT_ID')) then
        execute 'alter table INSUR_POLICY_SVD add PLAT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер строения " (309001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'UNOM')) then
        execute 'alter table INSUR_POLICY_SVD add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер квартиры  в доме" (309001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'UNKVA')) then
        execute 'alter table INSUR_POLICY_SVD add UNKVA VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры" (309001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'NOM')) then
        execute 'alter table INSUR_POLICY_SVD add NOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Индекс квартиры" (309001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'NOMI')) then
        execute 'alter table INSUR_POLICY_SVD add NOMI VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры" (309001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'KVNOM')) then
        execute 'alter table INSUR_POLICY_SVD add KVNOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Количество жилых помещений в квартире" (309001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'KOLGP')) then
        execute 'alter table INSUR_POLICY_SVD add KOLGP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код статуса жилого помещения" (309001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'FLATSTATUS')) then
        execute 'alter table INSUR_POLICY_SVD add FLATSTATUS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Общая площадь квартиры" (309001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'FOPL')) then
        execute 'alter table INSUR_POLICY_SVD add FOPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Подлежащая страхованию площадь жилого помещения" (309002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'OPL')) then
        execute 'alter table INSUR_POLICY_SVD add OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Код плательщика" (309002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'KODPL')) then
        execute 'alter table INSUR_POLICY_SVD add KODPL INT8';
    end if;
end $$;


-- Перенос колонок показателя "Лицевой счет" (309002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'LS')) then
        execute 'alter table INSUR_POLICY_SVD add LS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер страхового полиса/уникальный номер страхового свидетельства" (309002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'NPOL')) then
        execute 'alter table INSUR_POLICY_SVD add NPOL VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора " (309002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'DAT')) then
        execute 'alter table INSUR_POLICY_SVD add DAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Страховой взнос за 1 кв. м. в месяц" (309002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'VZNOS')) then
        execute 'alter table INSUR_POLICY_SVD add VZNOS NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Справочник условия страхования" (309002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'PRALT')) then
        execute 'alter table INSUR_POLICY_SVD add PRALT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'PRALT_CODE')) then
        execute 'alter table INSUR_POLICY_SVD add PRALT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак договора (1– страховщик несет ответственность, 2 – страховщик не несет ответственности)" (309002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'PR')) then
        execute 'alter table INSUR_POLICY_SVD add PR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма" (309002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'SS')) then
        execute 'alter table INSUR_POLICY_SVD add SS NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Количество свидетельств в квартире" (309002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'KOLDS')) then
        execute 'alter table INSUR_POLICY_SVD add KOLDS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сумма страхового взноса, уплаченного в отчетном месяце" (309003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'SOPLVZ')) then
        execute 'alter table INSUR_POLICY_SVD add SOPLVZ NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Дата досрочного погашения договора" (309003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'DAT_END')) then
        execute 'alter table INSUR_POLICY_SVD add DAT_END TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Тип собственности" (309003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'TYPE_PROP')) then
        execute 'alter table INSUR_POLICY_SVD add TYPE_PROP VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_POLICY_SVD', 'TYPE_PROP_CODE')) then
        execute 'alter table INSUR_POLICY_SVD add TYPE_PROP_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (310000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'EMP_ID')) then
        execute 'alter table INSUR_ALL_PROPERTY add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на ФСП INSUR_FSP_Q.EMP_ID" (310000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'FSP_ID')) then
        execute 'alter table INSUR_ALL_PROPERTY add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код страховой организации – по справочнику «Страховые организации»" (310000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'INSURANCE_ID')) then
        execute 'alter table INSUR_ALL_PROPERTY add INSURANCE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код административного округа " (310000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'OKRUG_ID')) then
        execute 'alter table INSUR_ALL_PROPERTY add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер строения " (310000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'UNOM')) then
        execute 'alter table INSUR_ALL_PROPERTY add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код формы объединения собственников (6 – ЖСК, ЖК,  7 – ТСЖ, 8 – БО (без объединения собственников), 0 – нет данных)" (310000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'ORG_TYPE')) then
        execute 'alter table INSUR_ALL_PROPERTY add ORG_TYPE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'ORG_TYPE_CODE')) then
        execute 'alter table INSUR_ALL_PROPERTY add ORG_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код управляющей организации (по справочнику «Управляющие организации»)" (310000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'SUBJECT_ID')) then
        execute 'alter table INSUR_ALL_PROPERTY add SUBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей организации)" (310000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'NAME')) then
        execute 'alter table INSUR_ALL_PROPERTY add NAME VARCHAR(2000)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер договора страхования" (310000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'NDOG')) then
        execute 'alter table INSUR_ALL_PROPERTY add NDOG VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора страхования " (310001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'NDOGDAT')) then
        execute 'alter table INSUR_ALL_PROPERTY add NDOGDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Страховая стоимость конструктивных элементов и помещений общего пользования" (310001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'ST1')) then
        execute 'alter table INSUR_ALL_PROPERTY add ST1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая стоимость внеквартирного инженерного оборудования" (310001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'ST2')) then
        execute 'alter table INSUR_ALL_PROPERTY add ST2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая стоимость лифтового оборудования" (310001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'ST3')) then
        execute 'alter table INSUR_ALL_PROPERTY add ST3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма конструктивных элементов и помещений общего пользования" (310001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'SS1')) then
        execute 'alter table INSUR_ALL_PROPERTY add SS1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма внеквартирного инженерного оборудования" (310001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'SS2')) then
        execute 'alter table INSUR_ALL_PROPERTY add SS2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма лифтового оборудования" (310001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'SS3')) then
        execute 'alter table INSUR_ALL_PROPERTY add SS3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Доля ответственности страховой организации в возмещении ущерба" (310001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'PART')) then
        execute 'alter table INSUR_ALL_PROPERTY add PART NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Доля города Москвы в праве на общее имущество" (310001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'PART_CITY')) then
        execute 'alter table INSUR_ALL_PROPERTY add PART_CITY NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Признак рассрочки платежа" (310001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'PAYSIGN')) then
        execute 'alter table INSUR_ALL_PROPERTY add PAYSIGN VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'PAYSIGN_CODE')) then
        execute 'alter table INSUR_ALL_PROPERTY add PAYSIGN_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Рассчитанный размер страховой премии" (310002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'RAS_PRIPAY')) then
        execute 'alter table INSUR_ALL_PROPERTY add RAS_PRIPAY NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_INPUT_FILE" (310002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_ALL_PROPERTY add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта (ссылка на INSUR_BUILDING)" (310002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'OBJ_ID')) then
        execute 'alter table INSUR_ALL_PROPERTY add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус" (310002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'STATUS')) then
        execute 'alter table INSUR_ALL_PROPERTY add STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_ALL_PROPERTY', 'STATUS_CODE')) then
        execute 'alter table INSUR_ALL_PROPERTY add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (311000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'EMP_ID')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_ALL_PROPERTY" (311000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'CONTRACT_ID')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add CONTRACT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код страховой организации" (311000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'KOD')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add KOD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер строения " (311000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'UNOM')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер договора страхования" (311000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'NDOG')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add NDOG VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора страхования " (311000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'NDOGDAT')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add NDOGDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер дополнительного соглашения" (311000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'NDOPS')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add NDOPS NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая стоимость конструктивных элементов и помещений общего пользования (новое значение по доп соглашению)" (311000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'ST1_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add ST1_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая стоимость внеквартирного инженерного оборудования (новое значение по доп соглашению)" (311000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'ST2_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add ST2_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая стоимость лифтового оборудования(новое значение по доп соглашению)" (311001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'ST3_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add ST3_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма конструктивных элементов и помещений общего пользования(новое значение по доп соглашению)" (311001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'SS1_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add SS1_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма внеквартирного инженерного оборудования(новое значение по доп соглашению)" (311001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'SS2_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add SS2_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма лифтового оборудования(новое значение по доп соглашению)" (311001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'SS3_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add SS3_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Доля ответственности страховой организации в возмещении ущерба(новое значение по доп соглашению)" (311001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'PART_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add PART_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Доля города Москвы в праве на общее имущество(новое значение по доп соглашению)" (311001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'PART_CITY_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add PART_CITY_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Признак рассрочки платежа" (311001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'PAYSIGN_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add PAYSIGN_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Рассчитанный размер страховой премии" (311001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'RAS_PRIPAY_NEW')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add RAS_PRIPAY_NEW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_INPUT_FILE" (311001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOP_ALL_PROPERTY', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_DOP_ALL_PROPERTY add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (312000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'EMP_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта (Ссылка на реестр зданий)" (312000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'OBJ_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор договора , ссылка на INSUR_ALL_ PROPERTY" (312000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'CONTRACT_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add CONTRACT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор страховой организации – по справочнику «Страховые организации»" (312000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'INSURANCE_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add INSURANCE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Доля ответственности страховой организации по возмещению вреда" (312000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'PART_СОMPENSATION')) then
        execute 'alter table INSUR_PARAM_CALCULATION add PART_СОMPENSATION NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Действительная стоимость дома, руб" (312000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'ACTUAL_COST')) then
        execute 'alter table INSUR_PARAM_CALCULATION add ACTUAL_COST NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Коэффициент пересчета действительной стоимости" (312000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'COEF_ACTUAL_COST')) then
        execute 'alter table INSUR_PARAM_CALCULATION add COEF_ACTUAL_COST NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Действительная стоимость дома (в пересчете на текущую цену), руб" (312000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'ACTUAL_COST_CURRENT')) then
        execute 'alter table INSUR_PARAM_CALCULATION add ACTUAL_COST_CURRENT NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Показатель рациональности объемно-планировочного решения, R" (312000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'INDICATOR_R')) then
        execute 'alter table INSUR_PARAM_CALCULATION add INDICATOR_R NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Расчетная площадь для определения стоимости общего имущества в МКД, кв.м." (312001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'CALCULATED_AREA')) then
        execute 'alter table INSUR_PARAM_CALCULATION add CALCULATED_AREA NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Земляные работы, фундамент " (312001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Стены и перегородки" (312001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Перекрытия" (312001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_3')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Полы" (312001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_4')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_4 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Проемы" (312001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_5')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_5 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Отделочные работы" (312001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_6')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_6 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Прочие" (312001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_7')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_7 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Крыша" (312001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_8')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_8 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Санитарно-технические работы и внутридомовое инженерное оборудование (исключая лифты)" (312001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_9')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_9 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Лифты и лифтовое оборудование" (312002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_10')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_10 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Всего" (312002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'UI_11')) then
        execute 'alter table INSUR_PARAM_CALCULATION add UI_11 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Общая стоимость конструкций без санитарно-технических работ и внутридомового инженерного оборудования" (312002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'TOTAL_COST_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add TOTAL_COST_1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Сумма конструкций без санитарно-технических работ и внутридомового инженерного оборудования" (312002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'DESIGN_COST_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add DESIGN_COST_1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Базовый тариф " (312002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'BASIC_RATE_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add BASIC_RATE_1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Годовая премия " (312002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'ANNUAL_BONUS_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add ANNUAL_BONUS_1 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Общая стоимость конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию" (312002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'TOTAL_COST_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add TOTAL_COST_2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Сумма конструкций по санитарно-техническим работам и внутридомовому инженерному оборудованию" (312002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'DESIGN_COST_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add DESIGN_COST_2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Базовый тариф" (312002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'BASIC_RATE_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add BASIC_RATE_2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Годовая премия" (312002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'ANNUAL_BONUS_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add ANNUAL_BONUS_2 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Общая стоимость конструкций лифтов и лифтового оборудования" (312003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'TOTAL_COST_3')) then
        execute 'alter table INSUR_PARAM_CALCULATION add TOTAL_COST_3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Сумма конструкций лифтов и лифтового оборудования" (312003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'DESIGN_COST_3')) then
        execute 'alter table INSUR_PARAM_CALCULATION add DESIGN_COST_3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Базовый тариф" (312003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'BASIC_RATE_3')) then
        execute 'alter table INSUR_PARAM_CALCULATION add BASIC_RATE_3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Годовая премия" (312003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'ANNUAL_BONUS_3')) then
        execute 'alter table INSUR_PARAM_CALCULATION add ANNUAL_BONUS_3 NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Размер годовой премии по дому" (312003400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'SIZE_ANNUAL_BONUS')) then
        execute 'alter table INSUR_PARAM_CALCULATION add SIZE_ANNUAL_BONUS NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Номер заявки" (312003500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'REQUEST_NUMBER')) then
        execute 'alter table INSUR_PARAM_CALCULATION add REQUEST_NUMBER VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Дата заявки" (312003600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'REQUEST_DATE')) then
        execute 'alter table INSUR_PARAM_CALCULATION add REQUEST_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Примечание" (312003700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'NOTE')) then
        execute 'alter table INSUR_PARAM_CALCULATION add NOTE VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (312003800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'CREATED_DATE')) then
        execute 'alter table INSUR_PARAM_CALCULATION add CREATED_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата согласования" (312003900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'APPROVAL_DATE')) then
        execute 'alter table INSUR_PARAM_CALCULATION add APPROVAL_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, который создал расчет" (312004000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'CREATED_USER_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add CREATED_USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, который согласовал расчет" (312004100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'APPROVAL_USER_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add APPROVAL_USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус расчета" (312004200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'STATUS')) then
        execute 'alter table INSUR_PARAM_CALCULATION add STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'STATUS_CODE')) then
        execute 'alter table INSUR_PARAM_CALCULATION add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Удалено, да/нет (1/0)" (312004300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'DELETED')) then
        execute 'alter table INSUR_PARAM_CALCULATION add DELETED INT2';
    end if;
end $$;


-- Перенос колонок показателя "Страхователь" (312004400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'SUBJECT_ID')) then
        execute 'alter table INSUR_PARAM_CALCULATION add SUBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата заполнения 1 (пользователь создал расчет)" (312004500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'DATE_FILL_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add DATE_FILL_1 TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, создавший расчет" (312004600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'AGREEMENT_ID_1')) then
        execute 'alter table INSUR_PARAM_CALCULATION add AGREEMENT_ID_1 INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата заполнения 2 (пользователь согласовал расчет)" (312004700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'DATE_FILL_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add DATE_FILL_2 TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, согласовавший расчет" (312004800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PARAM_CALCULATION', 'AGREEMENT_ID_2')) then
        execute 'alter table INSUR_PARAM_CALCULATION add AGREEMENT_ID_2 INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (313000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'EMP_ID')) then
        execute 'alter table INSUR_DAMAGE add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор объекта (Ссылка на реестр зданий)" (313000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'OBJ_ID')) then
        execute 'alter table INSUR_DAMAGE add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313000300)

-- Перенос колонок показателя "Ссылка на справочник "Страховые компании"" (313000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'INSUR_ORG_ID')) then
        execute 'alter table INSUR_DAMAGE add INSUR_ORG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313000500)

-- Перенос колонок показателя "Исходящая дата дела от СК" (313000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'INSUR_DATE')) then
        execute 'alter table INSUR_DAMAGE add INSUR_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Исходящий номер дела от СК" (313000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'INSUR_NOM')) then
        execute 'alter table INSUR_DAMAGE add INSUR_NOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313000800)

-- Перенос колонок показателя "Поле не используется" (313001000)

-- Перенос колонок показателя "Дата ущерба" (313001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_DATA')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_DATA TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313001200)

-- Перенос колонок показателя "Причина ущерба для ЖП (на основании справочника «Причина ущерба для ЖП»)" (313001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_REASON_GP')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_REASON_GP VARCHAR(2000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_REASON_GP_CODE')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_REASON_GP_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313001400)

-- Перенос колонок показателя "Поле не используется" (313001500)

-- Перенос колонок показателя "Расчетная стоимость" (313001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'ESTIMATED_VALUE')) then
        execute 'alter table INSUR_DAMAGE add ESTIMATED_VALUE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Страховая сумма" (313001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'INSUR_SUM')) then
        execute 'alter table INSUR_DAMAGE add INSUR_SUM NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Доля ответсвенности города" (313001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'PART_TOWN')) then
        execute 'alter table INSUR_DAMAGE add PART_TOWN NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Выплата СК" (313001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'STRAH_PLAT')) then
        execute 'alter table INSUR_DAMAGE add STRAH_PLAT NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Доля ответственности СК" (313002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'PART_INSUR')) then
        execute 'alter table INSUR_DAMAGE add PART_INSUR NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Тип здания (выбор из справочника)" (313002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_BUILD')) then
        execute 'alter table INSUR_DAMAGE add TYPE_BUILD VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_BUILD_CODE')) then
        execute 'alter table INSUR_DAMAGE add TYPE_BUILD_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Плита (выбор из справочника)" (313002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_COOKER')) then
        execute 'alter table INSUR_DAMAGE add TYPE_COOKER VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_COOKER_CODE')) then
        execute 'alter table INSUR_DAMAGE add TYPE_COOKER_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Материал пола (выбор из справочника)" (313002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_FLOOR')) then
        execute 'alter table INSUR_DAMAGE add TYPE_FLOOR VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_FLOOR_CODE')) then
        execute 'alter table INSUR_DAMAGE add TYPE_FLOOR_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сумма ущерба" (313002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'SUM_DAMAGE')) then
        execute 'alter table INSUR_DAMAGE add SUM_DAMAGE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Размер субсидии" (313002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'SUBSIDY')) then
        execute 'alter table INSUR_DAMAGE add SUBSIDY NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Согласующий_1, кто производит расчет (Справочник "Производящие расчет")" (313002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'AGREEMENT_ID_1')) then
        execute 'alter table INSUR_DAMAGE add AGREEMENT_ID_1 INT8';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313002700)

-- Перенос колонок показателя "Согласующий_2, кто проверяет (справочник "Проверяющие")" (313002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'AGREEMENT_ID_2')) then
        execute 'alter table INSUR_DAMAGE add AGREEMENT_ID_2 INT8';
    end if;
end $$;


-- Перенос колонок показателя "Поле не используется" (313002900)

-- Перенос колонок показателя "Дата" (313003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DATE_INPUT')) then
        execute 'alter table INSUR_DAMAGE add DATE_INPUT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Причина ущерба для ОИ (на основании справочника «Причина ущерба для ОИ»)" (313003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_REASON_OI')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_REASON_OI VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_REASON_OI_CODE')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_REASON_OI_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сумма ущерба, расчетная в Системе" (313003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'CALCUL_DAMAGE')) then
        execute 'alter table INSUR_DAMAGE add CALCUL_DAMAGE NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Тип договора" (313003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_DOC')) then
        execute 'alter table INSUR_DAMAGE add TYPE_DOC VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'TYPE_DOC_CODE')) then
        execute 'alter table INSUR_DAMAGE add TYPE_DOC_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата заполнения_1" (313003600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DATE_FILL_1')) then
        execute 'alter table INSUR_DAMAGE add DATE_FILL_1 TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата заполнения_2" (313003700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DATE_FILL_2')) then
        execute 'alter table INSUR_DAMAGE add DATE_FILL_2 TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Основной согласующий (Выбор из справочника «Основной согласующий»)" (313005000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'MAIN_AGREEMENT_ID')) then
        execute 'alter table INSUR_DAMAGE add MAIN_AGREEMENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата заполнения_3" (313005100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DATE_FILL_MAIN')) then
        execute 'alter table INSUR_DAMAGE add DATE_FILL_MAIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Подписант акта (Выбор из справочника «Подписанты»)" (313005200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'SIGNATURE_ID')) then
        execute 'alter table INSUR_DAMAGE add SIGNATURE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата заполнения_4" (313005300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DATE_FILL_SIGNATURE')) then
        execute 'alter table INSUR_DAMAGE add DATE_FILL_SIGNATURE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус расчета ущерба" (313005900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_AMOUNT_STATUS')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_AMOUNT_STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'DAMAGE_AMOUNT_STATUS_CODE')) then
        execute 'alter table INSUR_DAMAGE add DAMAGE_AMOUNT_STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра объекта" (313006000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'OBJ_REESTR_ID')) then
        execute 'alter table INSUR_DAMAGE add OBJ_REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер дела в ГБУ" (313006100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'NOM_DOC')) then
        execute 'alter table INSUR_DAMAGE add NOM_DOC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата дела в ОПС" (313006200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'NOM_DATE')) then
        execute 'alter table INSUR_DAMAGE add NOM_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата поступления дела в ЦИПиЖС" (313006300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'DATE_INPUT_GBY')) then
        execute 'alter table INSUR_DAMAGE add DATE_INPUT_GBY TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Подпричины ущерба по ЖП (справочник, 12134)" (313006500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'SUBREASON_DAMAGE_REASON')) then
        execute 'alter table INSUR_DAMAGE add SUBREASON_DAMAGE_REASON VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'SUBREASON_DAMAGE_REASON_CODE')) then
        execute 'alter table INSUR_DAMAGE add SUBREASON_DAMAGE_REASON_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уточнение Подпричины ущерба (справочник, 12135)" (313006600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE', 'REFINEMENT_SUBREASON')) then
        execute 'alter table INSUR_DAMAGE add REFINEMENT_SUBREASON VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE', 'REFINEMENT_SUBREASON_CODE')) then
        execute 'alter table INSUR_DAMAGE add REFINEMENT_SUBREASON_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (314000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'EMP_ID')) then
        execute 'alter table INSUR_PAY_TO add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на ФСП INSUR_FSP_Q.EMP_ID" (314000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'FSP_ID')) then
        execute 'alter table INSUR_PAY_TO add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "-Ссылка на  запись в INSUR_POLICY_SVD или  INSUR_ALL_PROPERTY" (314000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'CONTRACT_ID')) then
        execute 'alter table INSUR_PAY_TO add CONTRACT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра:" (314000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'ID_REESTR_CONTR')) then
        execute 'alter table INSUR_PAY_TO add ID_REESTR_CONTR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип договора " (314000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'TYPE_DOC')) then
        execute 'alter table INSUR_PAY_TO add TYPE_DOC VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_PAY_TO', 'TYPE_DOC_CODE')) then
        execute 'alter table INSUR_PAY_TO add TYPE_DOC_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Первый день месяца, за который предоставляются данные" (314000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'PERIOD')) then
        execute 'alter table INSUR_PAY_TO add PERIOD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на справочник «Страховые организации»" (314000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'INSURANCE_ORGANIZATION_ID')) then
        execute 'alter table INSUR_PAY_TO add INSURANCE_ORGANIZATION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код административного округа*" (314000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'AOK')) then
        execute 'alter table INSUR_PAY_TO add AOK INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер дома*" (314000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'UNOM')) then
        execute 'alter table INSUR_PAY_TO add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры*" (314001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'NOM')) then
        execute 'alter table INSUR_PAY_TO add NOM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Индекс квартиры*" (314001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'NOMI')) then
        execute 'alter table INSUR_PAY_TO add NOMI VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер полиса или страхового свидетельства" (314001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'NPOL')) then
        execute 'alter table INSUR_PAY_TO add NPOL VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора" (314001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'NPOLDATE')) then
        execute 'alter table INSUR_PAY_TO add NPOLDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер акта осмотра жилого помещения/объекта общего имущества" (314001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'COMNUMBER')) then
        execute 'alter table INSUR_PAY_TO add COMNUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата акта осмотра жилого помещения/объекта общего имущества" (314001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'COMDATE')) then
        execute 'alter table INSUR_PAY_TO add COMDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Размер ущерба" (314001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'SL')) then
        execute 'alter table INSUR_PAY_TO add SL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Сумма фактически выплаченного страхового                                              возмещения" (314001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'SP_FACT')) then
        execute 'alter table INSUR_PAY_TO add SP_FACT NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Размер удержанной части страхового возмещения" (314001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'SP_NO')) then
        execute 'alter table INSUR_PAY_TO add SP_NO NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Номер платежного поручения страховой организации" (314001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'FACTNUMBER')) then
        execute 'alter table INSUR_PAY_TO add FACTNUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата платежного поручения страховой организации" (314002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'FACTDATE')) then
        execute 'alter table INSUR_PAY_TO add FACTDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей компании) " (314002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'SUBJECT_ID')) then
        execute 'alter table INSUR_PAY_TO add SUBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер договора страхования общего имущества" (314002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'NDOC')) then
        execute 'alter table INSUR_PAY_TO add NDOC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора" (314002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'NDOGDAT')) then
        execute 'alter table INSUR_PAY_TO add NDOGDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_INPUT_FILE" (314002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_PAY_TO add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на объект" (314002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'OBJ_ID')) then
        execute 'alter table INSUR_PAY_TO add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра объекта" (314002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'OBJ_REESTR_ID')) then
        execute 'alter table INSUR_PAY_TO add OBJ_REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр дел" (314002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PAY_TO', 'LINK_DAMAGE_ID')) then
        execute 'alter table INSUR_PAY_TO add LINK_DAMAGE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (315000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'EMP_ID')) then
        execute 'alter table INSUR_NO_PAY add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на ФСП INSUR_FSP_Q.EMP_ID" (315000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'FSP_ID')) then
        execute 'alter table INSUR_NO_PAY add FSP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на  запись в INSUR_POLICY_SVD или  INSUR_ALL_PROPERTY" (315000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'CONTRACT_ID')) then
        execute 'alter table INSUR_NO_PAY add CONTRACT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра:" (315000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'ID_REESTR_CONTR')) then
        execute 'alter table INSUR_NO_PAY add ID_REESTR_CONTR INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип договора " (315000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'TYPE_DOC')) then
        execute 'alter table INSUR_NO_PAY add TYPE_DOC VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_NO_PAY', 'TYPE_DOC_CODE')) then
        execute 'alter table INSUR_NO_PAY add TYPE_DOC_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Первый день месяца, за который предоставляются данные" (315001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_NO_PAY add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на справочник «Страховые организации»" (315001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'INSURANCE_ORGANIZATION_ID')) then
        execute 'alter table INSUR_NO_PAY add INSURANCE_ORGANIZATION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код административного округа" (315001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'OKRUG_ID')) then
        execute 'alter table INSUR_NO_PAY add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер полиса или страхового свидетельства" (315001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'NPOL')) then
        execute 'alter table INSUR_NO_PAY add NPOL VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора" (315001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'NPOLDATE')) then
        execute 'alter table INSUR_NO_PAY add NPOLDATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата страхового события" (315001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'EVENTDAT')) then
        execute 'alter table INSUR_NO_PAY add EVENTDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата заявления о страховом событии" (315001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'APPDAT')) then
        execute 'alter table INSUR_NO_PAY add APPDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Причины отказа в страховой выплате – по справочнику «Причины отказа в выплате по договору страхования жилого помещения» /«Причины отказа в выплате по договору страхования общего помещения»" (315001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'REJECT')) then
        execute 'alter table INSUR_NO_PAY add REJECT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_NO_PAY', 'REJECT_CODE')) then
        execute 'alter table INSUR_NO_PAY add REJECT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер письма страховой организации об отказе в страховой выплате" (315001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'RENUMBER')) then
        execute 'alter table INSUR_NO_PAY add RENUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата письма страховой организации об отказе в страховой выплате" (315002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'REDAT')) then
        execute 'alter table INSUR_NO_PAY add REDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер строения " (315002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'UNOM')) then
        execute 'alter table INSUR_NO_PAY add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код формы объединения собственников (6-ЖСК,ЖК, 7-ТСЖ, 8-БО (без объединения собственников), 0-нет данных)" (315002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'ORG_TYPE')) then
        execute 'alter table INSUR_NO_PAY add ORG_TYPE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_NO_PAY', 'ORG_TYPE_CODE')) then
        execute 'alter table INSUR_NO_PAY add ORG_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код управляющей организации (по справочнику «Управляющие организации»)" (315002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'SUBJECT_ID')) then
        execute 'alter table INSUR_NO_PAY add SUBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование страхователя (ЖСК, ЖК, ТСЖ, управляющей организации)" (315002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'NAME')) then
        execute 'alter table INSUR_NO_PAY add NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер договора страхования" (315002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'NDOG')) then
        execute 'alter table INSUR_NO_PAY add NDOG VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия договора страхования " (315002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'NDOGDAT')) then
        execute 'alter table INSUR_NO_PAY add NDOGDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата сообщения о страховом событии" (315002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'PHONEDAT')) then
        execute 'alter table INSUR_NO_PAY add PHONEDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер акта осмотра объекта по заявленному событию" (315002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'INSPNUMBER')) then
        execute 'alter table INSUR_NO_PAY add INSPNUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата акта осмотра объекта по заявленному событию" (315002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'INSPDAT')) then
        execute 'alter table INSUR_NO_PAY add INSPDAT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код предполагаемой причины страхового события – по справочнику  «Причины страховых событий по договору страхования общего имущества»" (315003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'REASON')) then
        execute 'alter table INSUR_NO_PAY add REASON VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_NO_PAY', 'REASON_CODE')) then
        execute 'alter table INSUR_NO_PAY add REASON_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код причины отсутствия решения по страховому событию – по справочнику “Причины отсутствия решения о страховой выплате по договору страхования общего имущества»" (315003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'REASABS')) then
        execute 'alter table INSUR_NO_PAY add REASABS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_NO_PAY', 'REASABS_CODE')) then
        execute 'alter table INSUR_NO_PAY add REASABS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_INPUT_FILE" (315003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'LINK_ID_FILE')) then
        execute 'alter table INSUR_NO_PAY add LINK_ID_FILE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на объект" (315003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'OBJ_ID')) then
        execute 'alter table INSUR_NO_PAY add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра объекта" (315003400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_NO_PAY', 'OBJ_REESTR_ID')) then
        execute 'alter table INSUR_NO_PAY add OBJ_REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (316000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'EMP_ID')) then
        execute 'alter table INSUR_BUILDING_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки данных" (316000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LOAD_DATE')) then
        execute 'alter table INSUR_BUILDING_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер" (316000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'CADASTR_NUM')) then
        execute 'alter table INSUR_BUILDING_Q add CADASTR_NUM VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на номер из справочника «Статус объекта ЕГРН»" (316000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'STATUS_EGRN')) then
        execute 'alter table INSUR_BUILDING_Q add STATUS_EGRN VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'STATUS_EGRN_CODE')) then
        execute 'alter table INSUR_BUILDING_Q add STATUS_EGRN_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на номер из справочника «Статус состояния БТИ»" (316000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'STATUS_SOST_BTI')) then
        execute 'alter table INSUR_BUILDING_Q add STATUS_SOST_BTI VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'STATUS_SOST_BTI_CODE')) then
        execute 'alter table INSUR_BUILDING_Q add STATUS_SOST_BTI_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата постановки на кадастровый учет" (316000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'CADASTR_DATE')) then
        execute 'alter table INSUR_BUILDING_Q add CADASTR_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор округа из справочника округов" (316000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'OKRUG_ID')) then
        execute 'alter table INSUR_BUILDING_Q add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор района из справочника районов" (316000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'DISTRICT_ID')) then
        execute 'alter table INSUR_BUILDING_Q add DISTRICT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "UNOM" (316000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'UNOM')) then
        execute 'alter table INSUR_BUILDING_Q add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Справочник «Статус дома ГБУ» " (316001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'TYPE_MKD')) then
        execute 'alter table INSUR_BUILDING_Q add TYPE_MKD VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'TYPE_MKD_CODE')) then
        execute 'alter table INSUR_BUILDING_Q add TYPE_MKD_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Год постройки" (316001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'YEAR_STROI')) then
        execute 'alter table INSUR_BUILDING_Q add YEAR_STROI INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во этажей" (316001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'COUNT_FLOOR')) then
        execute 'alter table INSUR_BUILDING_Q add COUNT_FLOOR INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во квартир в доме" (316001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'KOL_GP')) then
        execute 'alter table INSUR_BUILDING_Q add KOL_GP INT4';
    end if;
end $$;


-- Перенос колонок показателя "Общая площадь" (316001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'OPL')) then
        execute 'alter table INSUR_BUILDING_Q add OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь жилых помещений" (316001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'OPL_G')) then
        execute 'alter table INSUR_BUILDING_Q add OPL_G NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь нежилых помещений" (316001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'OPL_N')) then
        execute 'alter table INSUR_BUILDING_Q add OPL_N NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь балконов" (316001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'BPL')) then
        execute 'alter table INSUR_BUILDING_Q add BPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь холодных помещений" (316001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'HPL')) then
        execute 'alter table INSUR_BUILDING_Q add HPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь лоджий" (316001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LPL')) then
        execute 'alter table INSUR_BUILDING_Q add LPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во лифтов пассажирских" (316002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LFPQ')) then
        execute 'alter table INSUR_BUILDING_Q add LFPQ INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во лифтов грузопассажирских" (316002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LFGPQ')) then
        execute 'alter table INSUR_BUILDING_Q add LFGPQ INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во лифтов грузовых" (316002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LFGQ')) then
        execute 'alter table INSUR_BUILDING_Q add LFGQ INT2';
    end if;
end $$;


-- Перенос колонок показателя "GUID-ссылка в справочнике ФИАС на адрес МКД" (316002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'GUID_FIAS_MKD')) then
        execute 'alter table INSUR_BUILDING_Q add GUID_FIAS_MKD VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на Реестр связей с объектами БТИ  " (316002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LINK_BTI_FSKS')) then
        execute 'alter table INSUR_BUILDING_Q add LINK_BTI_FSKS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на Реестр зданий  в Росреестре" (316002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'LINK_EGRN_BILD')) then
        execute 'alter table INSUR_BUILDING_Q add LINK_EGRN_BILD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Источник заполнения" (316002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'SOURCE_INPUT')) then
        execute 'alter table INSUR_BUILDING_Q add SOURCE_INPUT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'SOURCE_INPUT_CODE')) then
        execute 'alter table INSUR_BUILDING_Q add SOURCE_INPUT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак участия в программе страхования" (316002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'FLAG_INSUR')) then
        execute 'alter table INSUR_BUILDING_Q add FLAG_INSUR INT2';
    end if;
end $$;


-- Перенос колонок показателя "Назначение объекта" (316002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'PURPOSE_NAME')) then
        execute 'alter table INSUR_BUILDING_Q add PURPOSE_NAME VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'PURPOSE_NAME_CODE')) then
        execute 'alter table INSUR_BUILDING_Q add PURPOSE_NAME_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь кровли" (316003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'KROVPL')) then
        execute 'alter table INSUR_BUILDING_Q add KROVPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Строительная стоимость" (316003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'STROI_PRICE')) then
        execute 'alter table INSUR_BUILDING_Q add STROI_PRICE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на адрес МКД" (316004400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'ADDRESS_ID')) then
        execute 'alter table INSUR_BUILDING_Q add ADDRESS_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата снятия с кадастрового учета" (316004500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'CADASTR_REMOVE')) then
        execute 'alter table INSUR_BUILDING_Q add CADASTR_REMOVE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код КЛАДР" (316004600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'CODE_KLADR')) then
        execute 'alter table INSUR_BUILDING_Q add CODE_KLADR VARCHAR(25)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь помещений, не входящих в общую зону, кв.м." (316004700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'EPL')) then
        execute 'alter table INSUR_BUILDING_Q add EPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Процент износа" (316004800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'PIZN')) then
        execute 'alter table INSUR_BUILDING_Q add PIZN NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Источник атрибутов" (316004900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BUILDING_Q', 'SOURCE_ATRIB')) then
        execute 'alter table INSUR_BUILDING_Q add SOURCE_ATRIB VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (317000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'EMP_ID')) then
        execute 'alter table INSUR_FLAT_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки данных" (317000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'LOAD_DATE')) then
        execute 'alter table INSUR_FLAT_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер" (317000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'CADASTR_NUM')) then
        execute 'alter table INSUR_FLAT_Q add CADASTR_NUM VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "UNOM" (317000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'UNOM')) then
        execute 'alter table INSUR_FLAT_Q add UNOM INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер квартиры" (317000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'KVNOM')) then
        execute 'alter table INSUR_FLAT_Q add KVNOM VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Назначение помещения" (317000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'KLASS_FLAT')) then
        execute 'alter table INSUR_FLAT_Q add KLASS_FLAT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FLAT_Q', 'KLASS_FLAT_CODE')) then
        execute 'alter table INSUR_FLAT_Q add KLASS_FLAT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип помещения" (317000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'TYPE_FLAT')) then
        execute 'alter table INSUR_FLAT_Q add TYPE_FLAT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FLAT_Q', 'TYPE_FLAT_CODE')) then
        execute 'alter table INSUR_FLAT_Q add TYPE_FLAT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус жилого помещения" (317001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'FLATSTATUS')) then
        execute 'alter table INSUR_FLAT_Q add FLATSTATUS INT2';
    end if;
end $$;


-- Перенос колонок показателя "Признак коммунальности квартиры" (317001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'PRKOM')) then
        execute 'alter table INSUR_FLAT_Q add PRKOM INT2';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во комнат в квартире" (317001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'KOL_GP')) then
        execute 'alter table INSUR_FLAT_Q add KOL_GP INT2';
    end if;
end $$;


-- Перенос колонок показателя "Общая площадь" (317001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'FOPL')) then
        execute 'alter table INSUR_FLAT_Q add FOPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь с летними" (317001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'PPL')) then
        execute 'alter table INSUR_FLAT_Q add PPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Площадь жилая" (317001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'GPL')) then
        execute 'alter table INSUR_FLAT_Q add GPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Код ФИАС помещения" (317001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'GUID_FIAS_FLAT')) then
        execute 'alter table INSUR_FLAT_Q add GUID_FIAS_FLAT VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код ФИАС МКД" (317001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'GUID_FIAS_MKD')) then
        execute 'alter table INSUR_FLAT_Q add GUID_FIAS_MKD VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на Реестр связей с помещением БТИ  " (317001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'LINK_BTI_FLAT')) then
        execute 'alter table INSUR_FLAT_Q add LINK_BTI_FLAT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на Реестр зданий  в Росреестре" (317001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'LINK_INSUR_EGRN')) then
        execute 'alter table INSUR_FLAT_Q add LINK_INSUR_EGRN INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на МКД" (317002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'LINK_OBJECT_MKD')) then
        execute 'alter table INSUR_FLAT_Q add LINK_OBJECT_MKD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Источник заполнения" (317002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'SOURCE_INPUT')) then
        execute 'alter table INSUR_FLAT_Q add SOURCE_INPUT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FLAT_Q', 'SOURCE_INPUT_CODE')) then
        execute 'alter table INSUR_FLAT_Q add SOURCE_INPUT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак участия в программе страхования" (317002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'FLAG_INSUR')) then
        execute 'alter table INSUR_FLAT_Q add FLAG_INSUR INT2';
    end if;
end $$;


-- Перенос колонок показателя "Адрес МКД в ФИАС" (317002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'ADDRESS_FIAS_MKD')) then
        execute 'alter table INSUR_FLAT_Q add ADDRESS_FIAS_MKD VARCHAR(500)';
    end if;
end $$;


-- Перенос колонок показателя "Тип помещения (по назначению)" (317002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'TYPE_FLAT_2')) then
        execute 'alter table INSUR_FLAT_Q add TYPE_FLAT_2 VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FLAT_Q', 'TYPE_FLAT_2_CODE')) then
        execute 'alter table INSUR_FLAT_Q add TYPE_FLAT_2_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата постановки на кадастровый учет" (317002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'CADASTR_DATE')) then
        execute 'alter table INSUR_FLAT_Q add CADASTR_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус объекта ЕГРН" (317002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'STATUS_EGRN')) then
        execute 'alter table INSUR_FLAT_Q add STATUS_EGRN VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FLAT_Q', 'STATUS_EGRN_CODE')) then
        execute 'alter table INSUR_FLAT_Q add STATUS_EGRN_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "(НЕ ИСПОЛЬЗОВАТЬ) Общая площадь" (317002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'OPL')) then
        execute 'alter table INSUR_FLAT_Q add OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Дата снятия с кадастрового учета" (317003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'CADASTR_REMOVE')) then
        execute 'alter table INSUR_FLAT_Q add CADASTR_REMOVE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Код КЛАДР" (317003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'CODE_KLADR')) then
        execute 'alter table INSUR_FLAT_Q add CODE_KLADR VARCHAR(25)';
    end if;
end $$;


-- Перенос колонок показателя "Источники атрибутов" (317003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_Q', 'SOURCE_ATRIB')) then
        execute 'alter table INSUR_FLAT_Q add SOURCE_ATRIB VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (319000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'EMP_ID')) then
        execute 'alter table INSUR_ADDRESS add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "GUID-ссылка в справочнике ФИАС на адрес МКД" (319000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'GUID_FIAS_HOUSE')) then
        execute 'alter table INSUR_ADDRESS add GUID_FIAS_HOUSE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Полный адрес МКД по справочнику ФИАС" (319000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'FULL_ADDRESS')) then
        execute 'alter table INSUR_ADDRESS add FULL_ADDRESS VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Город" (319000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'CITY')) then
        execute 'alter table INSUR_ADDRESS add CITY VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Улица" (319000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'STREET')) then
        execute 'alter table INSUR_ADDRESS add STREET VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дом" (319000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'HOUSE')) then
        execute 'alter table INSUR_ADDRESS add HOUSE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Корпус" (319000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'CORPUS')) then
        execute 'alter table INSUR_ADDRESS add CORPUS VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Строение" (319000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'STRUCTURE')) then
        execute 'alter table INSUR_ADDRESS add STRUCTURE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Регион" (319000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'REGION')) then
        execute 'alter table INSUR_ADDRESS add REGION VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "GUID-ссылка в справочнике ФИАС на адрес улицы" (319001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'GUID_FIAS_STREET')) then
        execute 'alter table INSUR_ADDRESS add GUID_FIAS_STREET VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Источник поступления адреса" (319001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ADDRESS', 'SOURCE_ADDRESS')) then
        execute 'alter table INSUR_ADDRESS add SOURCE_ADDRESS VARCHAR(10)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_ADDRESS', 'SOURCE_ADDRESS_CODE')) then
        execute 'alter table INSUR_ADDRESS add SOURCE_ADDRESS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (320000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_OKRUG', 'ID')) then
        execute 'alter table INSUR_OKRUG add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Код" (320000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_OKRUG', 'CODE')) then
        execute 'alter table INSUR_OKRUG add CODE INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (320000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_OKRUG', 'NAME')) then
        execute 'alter table INSUR_OKRUG add NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Сокращенное наименование" (320000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_OKRUG', 'SHORT_NAME')) then
        execute 'alter table INSUR_OKRUG add SHORT_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор страховой компании" (320000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_OKRUG', 'INSURANCE_COMPANY_ID')) then
        execute 'alter table INSUR_OKRUG add INSURANCE_COMPANY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (321000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DISTRICT', 'ID')) then
        execute 'alter table INSUR_DISTRICT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (321000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DISTRICT', 'NAME')) then
        execute 'alter table INSUR_DISTRICT add NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код" (321000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DISTRICT', 'CODE')) then
        execute 'alter table INSUR_DISTRICT add CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор округа" (321000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DISTRICT', 'OKRUG_ID')) then
        execute 'alter table INSUR_DISTRICT add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (322000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_STORAGE', 'ID')) then
        execute 'alter table INSUR_FILE_STORAGE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование файла" (322000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_STORAGE', 'FILENAME')) then
        execute 'alter table INSUR_FILE_STORAGE add FILENAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Признак виртуальной директории" (322000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_STORAGE', 'IS_VIRTUAL_DIRECTORY')) then
        execute 'alter table INSUR_FILE_STORAGE add IS_VIRTUAL_DIRECTORY INT2';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор виртуальной папки" (322000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_STORAGE', 'VIRTUAL_DIRECTORY_ID')) then
        execute 'alter table INSUR_FILE_STORAGE add VIRTUAL_DIRECTORY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Период учета" (322000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_STORAGE', 'PERIOD_REG_DATE')) then
        execute 'alter table INSUR_FILE_STORAGE add PERIOD_REG_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Хэш" (322000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_STORAGE', 'HASH')) then
        execute 'alter table INSUR_FILE_STORAGE add HASH TEXT';
    end if;
end $$;


-- Перенос колонок показателя "ИД" (324000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'ID')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Признаки  ущерба" (324000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'DAMAGE_SYMPTOM')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add DAMAGE_SYMPTOM VARCHAR(355) not null';
    end if;
end $$;


-- Перенос колонок показателя "Материальный ущерб, %" (324000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'MATERIAL_DAMAGE')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add MATERIAL_DAMAGE VARCHAR(100) not null';
    end if;
end $$;


-- Перенос колонок показателя "Примерный состав работ для устранения ущерба" (324000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'WORK_COMPOSITION')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add WORK_COMPOSITION VARCHAR(255) not null';
    end if;
end $$;


-- Перенос колонок показателя "Элемент конструкции (описание)" (324000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'ELEMENT_CONSTRUCTION_DESCRIPTION')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add ELEMENT_CONSTRUCTION_DESCRIPTION VARCHAR(255) not null';
    end if;
end $$;


-- Перенос колонок показателя "Элемент конструкции (справочник)" (324000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'ELEMENT_CONSTRUCTION')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add ELEMENT_CONSTRUCTION VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'ELEMENT_CONSTRUCTION_CODE')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add ELEMENT_CONSTRUCTION_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Минимальный процент урона" (324000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'MATERIAL_DAMAGE_MIN')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add MATERIAL_DAMAGE_MIN INT8';
    end if;
end $$;


-- Перенос колонок показателя "Максимальный процент урона" (324000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_ASSESSMENT_METHOD', 'MATERIAL_DAMAGE_MAX')) then
        execute 'alter table INSUR_DAMAGE_ASSESSMENT_METHOD add MATERIAL_DAMAGE_MAX INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (326000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_BUILD_BTI', 'EMP_ID')) then
        execute 'alter table INSUR_LINK_BUILD_BTI add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на запись в Реестре зданий БТИ " (326000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_BUILD_BTI', 'ID_BTI_FSKS')) then
        execute 'alter table INSUR_LINK_BUILD_BTI add ID_BTI_FSKS INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на запись в Реестре объектов страхования МКД INSUR_BUILDING" (326000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_BUILD_BTI', 'ID_INSUR_BUILD')) then
        execute 'alter table INSUR_LINK_BUILD_BTI add ID_INSUR_BUILD INT8';
    end if;
end $$;


-- Перенос колонок показателя "1/0 ( Дублируется UNOM = да, нет)" (326000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_BUILD_BTI', 'FLAG_DUBL_UNOM')) then
        execute 'alter table INSUR_LINK_BUILD_BTI add FLAG_DUBL_UNOM INT2';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (327000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_FLAT_EGRN', 'EMP_ID')) then
        execute 'alter table INSUR_LINK_FLAT_EGRN add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на запись в Реестре зданий БТИ" (327000200)

-- Перенос колонок показателя "Ссылка на запись в Реестре объектов страхования МКД INSUR_BUILDING" (327000300)

-- Перенос колонок показателя "Уникальный идентификатор" (328000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSURANCE_ORGANIZATION', 'ID')) then
        execute 'alter table INSUR_INSURANCE_ORGANIZATION add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование страховой организации" (328000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSURANCE_ORGANIZATION', 'FULL_NAME')) then
        execute 'alter table INSUR_INSURANCE_ORGANIZATION add FULL_NAME VARCHAR(255) not null';
    end if;
end $$;


-- Перенос колонок показателя "Сокращенное наименование страховой компании" (328000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSURANCE_ORGANIZATION', 'SHORT_NAME')) then
        execute 'alter table INSUR_INSURANCE_ORGANIZATION add SHORT_NAME VARCHAR(50) not null';
    end if;
end $$;


-- Перенос колонок показателя "Код" (328000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSURANCE_ORGANIZATION', 'CODE')) then
        execute 'alter table INSUR_INSURANCE_ORGANIZATION add CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записиУникальный номер записи" (329000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PART_COMPENSATION', 'EMP_ID')) then
        execute 'alter table INSUR_PART_COMPENSATION add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия" (329000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PART_COMPENSATION', 'DATE_BEGIN')) then
        execute 'alter table INSUR_PART_COMPENSATION add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Тип отвественности СК и города" (329000300)

-- Перенос колонок показателя "Доля ответственности СК" (329000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PART_COMPENSATION', 'IC_VALUE')) then
        execute 'alter table INSUR_PART_COMPENSATION add IC_VALUE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Доля ответственности города" (329000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_PART_COMPENSATION', 'CITY_VALUE')) then
        execute 'alter table INSUR_PART_COMPENSATION add CITY_VALUE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (330000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BASE_TARIFF', 'EMP_ID')) then
        execute 'alter table INSUR_BASE_TARIFF add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (330000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BASE_TARIFF', 'NAME')) then
        execute 'alter table INSUR_BASE_TARIFF add NAME VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "Значение" (330000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BASE_TARIFF', 'VALUE')) then
        execute 'alter table INSUR_BASE_TARIFF add VALUE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (331000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'ID')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип плиты" (331000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'STOVE_TYPE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add STOVE_TYPE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'STOVE_TYPE_CODE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add STOVE_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Элемент конструкции" (331000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'ELEMENTS_CONSTRUCTIONS')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add ELEMENTS_CONSTRUCTIONS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'ELEMENTS_CONSTRUCTIONS_CODE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add ELEMENTS_CONSTRUCTIONS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Материал пола" (331000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'FLOOR_MATERIAL')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add FLOOR_MATERIAL VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'FLOOR_MATERIAL_CODE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add FLOOR_MATERIAL_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип здания" (331000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'BUILDING_TYPE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add BUILDING_TYPE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'BUILDING_TYPE_CODE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add BUILDING_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Удельный вес стоимости" (331000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'COST_VALUE')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add COST_VALUE NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификтаор родителя" (331000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INTEGRATED_INDICATORS_REPL_COST', 'PARENT_ID')) then
        execute 'alter table INSUR_INTEGRATED_INDICATORS_REPL_COST add PARENT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (332000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_STATUS', 'ID')) then
        execute 'alter table INSUR_FLAT_STATUS add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (332000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_STATUS', 'NAME')) then
        execute 'alter table INSUR_FLAT_STATUS add NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код" (332000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_STATUS', 'CODE')) then
        execute 'alter table INSUR_FLAT_STATUS add CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Краткое название" (332000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_STATUS', 'SHORT_NAME')) then
        execute 'alter table INSUR_FLAT_STATUS add SHORT_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (333000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_TYPE', 'ID')) then
        execute 'alter table INSUR_FLAT_TYPE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование" (333000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_TYPE', 'NAME')) then
        execute 'alter table INSUR_FLAT_TYPE add NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Код" (333000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_TYPE', 'CODE')) then
        execute 'alter table INSUR_FLAT_TYPE add CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Краткое название" (333000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FLAT_TYPE', 'SHORT_NAME')) then
        execute 'alter table INSUR_FLAT_TYPE add SHORT_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (334000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'EMP_ID')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, который получил проект договора" (334000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'GOT_USER_ID')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add GOT_USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата получения проекта договора" (334000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'GOT_DATE')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add GOT_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, который согласовал проект договора" (334000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'APPROVAL_USER_ID')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add APPROVAL_USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата согласования проекта договора" (334000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'APPROVAL_DATE')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add APPROVAL_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Примечание" (334000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'NOTE')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add NOTE VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор расчета" (334000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'CALCULATION_ID')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add CALCULATION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Для отчета Справка, пункт Замечания по документу" (334000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'COMMENT_SPRAVKA')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add COMMENT_SPRAVKA VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Для отчета Справка, пункт Принятое решение с учетом замечаний" (334000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'RESUME_SPRAVKA')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add RESUME_SPRAVKA VARCHAR(1000)';
    end if;
end $$;


-- Перенос колонок показателя "Доля города Москвы, %" (334001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'PART_MOSCOW')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add PART_MOSCOW NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Использовать расчет для первой категории" (334001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'KAT_1')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add KAT_1 INT2';
    end if;
end $$;


-- Перенос колонок показателя "Использовать расчет для второй категории" (334001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'KAT_2')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add KAT_2 INT2';
    end if;
end $$;


-- Перенос колонок показателя "Использовать расчет для третьей категории" (334001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'KAT_3')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add KAT_3 INT2';
    end if;
end $$;


-- Перенос колонок показателя "№ проекта договора" (334001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'PROGECT_NUM')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add PROGECT_NUM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Годовой страховой премии по дому" (334001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_AGREEMENT_PROJECT', 'SIZE_BONUS_MKD')) then
        execute 'alter table INSUR_AGREEMENT_PROJECT add SIZE_BONUS_MKD NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (340000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'EMP_ID')) then
        execute 'alter table INSUR_DOCUMENTS add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Вид документа-основания (выбор из справочника, справочник «Виды документов-оснований»)" (340000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_TYPE_ID')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Признак о предоставлении (Да/Нет)" (340000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_IS_HAVE')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_IS_HAVE INT2';
    end if;
end $$;


-- Перенос колонок показателя "Тип (бумажная/электронная)" (340000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_TYPE_M')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_TYPE_M VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_TYPE_M_CODE')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_TYPE_M_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер" (340000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_NUMBER')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_NUMBER VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата" (340000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_DATE')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Организация  (выбор из справочника, справочник  «Страховые организации)" (340000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DOC_ORG_ID')) then
        execute 'alter table INSUR_DOCUMENTS add DOC_ORG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ФИО сотрудника загрузившего документ" (340000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'FIO_SCAN')) then
        execute 'alter table INSUR_DOCUMENTS add FIO_SCAN VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на уникальный номер записи" (340000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'OBJ_ID')) then
        execute 'alter table INSUR_DOCUMENTS add OBJ_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сcылка на хранилище документов" (340001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'FILE_STORAGE_ID')) then
        execute 'alter table INSUR_DOCUMENTS add FILE_STORAGE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, создавший запись" (340001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'USER_ID')) then
        execute 'alter table INSUR_DOCUMENTS add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата ввода" (340001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'DATE_CREATE')) then
        execute 'alter table INSUR_DOCUMENTS add DATE_CREATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра записи" (340001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DOCUMENTS', 'REESTR_ID')) then
        execute 'alter table INSUR_DOCUMENTS add REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (344000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'EMP_ID')) then
        execute 'alter table INSUR_BANK add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Наименование банка" (344000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'BANK_NAME')) then
        execute 'alter table INSUR_BANK add BANK_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (344000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'DATE_INPUT')) then
        execute 'alter table INSUR_BANK add DATE_INPUT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ИНН банка" (344000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'INN')) then
        execute 'alter table INSUR_BANK add INN VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "КПП банка" (344000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'KPP')) then
        execute 'alter table INSUR_BANK add KPP VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "БИК банка" (344000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'BIC')) then
        execute 'alter table INSUR_BANK add BIC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Корреспондентский счет" (344000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_BANK', 'KOR_ACC')) then
        execute 'alter table INSUR_BANK add KOR_ACC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (345000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'EMP_ID')) then
        execute 'alter table INSUR_SUBJECT add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр Округ" (345000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'OKRUG_ID')) then
        execute 'alter table INSUR_SUBJECT add OKRUG_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код организации" (345000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'KOD_ORG')) then
        execute 'alter table INSUR_SUBJECT add KOD_ORG INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код управляющей компании" (345000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'KOD_UPK')) then
        execute 'alter table INSUR_SUBJECT add KOD_UPK INT8';
    end if;
end $$;


-- Перенос колонок показателя "Название организации" (345000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'SUBJECT_NAME')) then
        execute 'alter table INSUR_SUBJECT add SUBJECT_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор организации" (345000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'ORG_ID_T')) then
        execute 'alter table INSUR_SUBJECT add ORG_ID_T VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Должность руководителя" (345000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'EMPL_ROLE')) then
        execute 'alter table INSUR_SUBJECT add EMPL_ROLE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "ФИО руководителя" (345000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'FIO_ADM')) then
        execute 'alter table INSUR_SUBJECT add FIO_ADM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Юридический адрес организации" (345000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'ORG_ADR_U')) then
        execute 'alter table INSUR_SUBJECT add ORG_ADR_U VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Физический адрес организации" (345001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'ORG_ADR_F')) then
        execute 'alter table INSUR_SUBJECT add ORG_ADR_F VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Телефон организации" (345001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'ORG_PHONE')) then
        execute 'alter table INSUR_SUBJECT add ORG_PHONE VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (345001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'DATE_INPUT')) then
        execute 'alter table INSUR_SUBJECT add DATE_INPUT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата рождения (для физ лиц)" (345001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'BIRTHDAY')) then
        execute 'alter table INSUR_SUBJECT add BIRTHDAY TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ИНН" (345001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'INN')) then
        execute 'alter table INSUR_SUBJECT add INN VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "КПП" (345001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'KPP')) then
        execute 'alter table INSUR_SUBJECT add KPP VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Расчетный счет" (345001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'RACH_ACC')) then
        execute 'alter table INSUR_SUBJECT add RACH_ACC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Лицевой счет" (345001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'LIC_ACC')) then
        execute 'alter table INSUR_SUBJECT add LIC_ACC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер банковской карточки" (345001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'NUM_CARD')) then
        execute 'alter table INSUR_SUBJECT add NUM_CARD VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер документа, удостоверяющего личность" (345001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'NOM_DOC')) then
        execute 'alter table INSUR_SUBJECT add NOM_DOC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата паспорта, удостоверяющего личность" (345002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'DATE_DOC')) then
        execute 'alter table INSUR_SUBJECT add DATE_DOC TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата выдачи документа, удостоверяющего личность" (345002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'DATE_IN_DOC')) then
        execute 'alter table INSUR_SUBJECT add DATE_IN_DOC TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Организация выдавшая документ, удостоверяющего личность" (345002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'ORG_DOC')) then
        execute 'alter table INSUR_SUBJECT add ORG_DOC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Тип субъекта (выбор из справочника «Тип субъекта» ­ 12142)" (345002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'TYPE_SUBJECT')) then
        execute 'alter table INSUR_SUBJECT add TYPE_SUBJECT VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_SUBJECT', 'TYPE_SUBJECT_CODE')) then
        execute 'alter table INSUR_SUBJECT add TYPE_SUBJECT_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на EMP_ID  в INSUR_BANK" (345002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SUBJECT', 'BANK_ID')) then
        execute 'alter table INSUR_SUBJECT add BANK_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (346000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSUR_RATE', 'ID')) then
        execute 'alter table INSUR_INSUR_RATE add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия тарифа" (346000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSUR_RATE', 'DATE_BEGIN')) then
        execute 'alter table INSUR_INSUR_RATE add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Размер тарифа" (346000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INSUR_RATE', 'TARIFF')) then
        execute 'alter table INSUR_INSUR_RATE add TARIFF NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (347000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMON_PROPERTY_TARIFF', 'ID')) then
        execute 'alter table INSUR_COMMON_PROPERTY_TARIFF add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия" (347000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMON_PROPERTY_TARIFF', 'DATE_BEGIN')) then
        execute 'alter table INSUR_COMMON_PROPERTY_TARIFF add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Категория" (347000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMON_PROPERTY_TARIFF', 'CATEGORY')) then
        execute 'alter table INSUR_COMMON_PROPERTY_TARIFF add CATEGORY INT8';
    end if;
end $$;


-- Перенос колонок показателя "Значение" (347000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMON_PROPERTY_TARIFF', 'VALUE')) then
        execute 'alter table INSUR_COMMON_PROPERTY_TARIFF add VALUE NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (348000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LIVING_PREMISE_INSUR_COST', 'ID')) then
        execute 'alter table INSUR_LIVING_PREMISE_INSUR_COST add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия" (348000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LIVING_PREMISE_INSUR_COST', 'DATE_BEGIN')) then
        execute 'alter table INSUR_LIVING_PREMISE_INSUR_COST add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Условие" (348000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LIVING_PREMISE_INSUR_COST', 'CONDITION')) then
        execute 'alter table INSUR_LIVING_PREMISE_INSUR_COST add CONDITION VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Значение, руб." (348000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LIVING_PREMISE_INSUR_COST', 'VALUE')) then
        execute 'alter table INSUR_LIVING_PREMISE_INSUR_COST add VALUE NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (349000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SHARE_RESPONSIBILITY_IC_CITY', 'ID')) then
        execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала  действия" (349000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SHARE_RESPONSIBILITY_IC_CITY', 'DATE_BEGIN')) then
        execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Тип" (349000300)

-- Перенос колонок показателя "Доля СК,%" (349000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SHARE_RESPONSIBILITY_IC_CITY', 'IC_SHARE')) then
        execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add IC_SHARE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Доля города,%" (349000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SHARE_RESPONSIBILITY_IC_CITY', 'CITY_SHARE')) then
        execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add CITY_SHARE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Примечание" (349000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_SHARE_RESPONSIBILITY_IC_CITY', 'NOTE')) then
        execute 'alter table INSUR_SHARE_RESPONSIBILITY_IC_CITY add NOTE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (350000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'EMP_ID')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на дело (INSUR_DAMAGE)" (350000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'DAMAGE_ID')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add DAMAGE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на справочник "Методики расчета ущерба"" (350000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'DAMAGE_ASSESSMENT_METHOD_ID')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add DAMAGE_ASSESSMENT_METHOD_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Материальный ущерб" (350000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'MATERIAL_DAMAGE')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add MATERIAL_DAMAGE NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Удельный вес восстановительной стоимости" (350000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'PROPORTION_REPLACEMENT_COST')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add PROPORTION_REPLACEMENT_COST NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Удельный вес поврежденного участка" (350000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'PROPORTION_DAMAGED_AREA')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add PROPORTION_DAMAGED_AREA NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Сумма ущерба" (350000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'DAMAGE_AMOUNT')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add DAMAGE_AMOUNT NUMERIC(10, 4)';
    end if;
end $$;


-- Перенос колонок показателя "Элемент конструкции" (350000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'ELEMENT_CONSTRUCTION')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add ELEMENT_CONSTRUCTION VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_DAMAGE_AMOUNT', 'ELEMENT_CONSTRUCTION_CODE')) then
        execute 'alter table INSUR_DAMAGE_AMOUNT add ELEMENT_CONSTRUCTION_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (351000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TARIFF', 'ID')) then
        execute 'alter table INSUR_TARIFF add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия тарифа" (351000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TARIFF', 'DATE_BEGIN')) then
        execute 'alter table INSUR_TARIFF add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Размер тарифа" (351000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TARIFF', 'VALUE')) then
        execute 'alter table INSUR_TARIFF add VALUE NUMERIC(18, 2)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (352000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'EMP_ID')) then
        execute 'alter table INSUR_CHANGES_LOG add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ID-записи, в которую вносятся изменения" (352000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'OBJECT_ID')) then
        execute 'alter table INSUR_CHANGES_LOG add OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра  для записи, в которую вносятся изменения" (352000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'REESTR_ID')) then
        execute 'alter table INSUR_CHANGES_LOG add REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата внесения изменения " (352000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'LOAD_DATE')) then
        execute 'alter table INSUR_CHANGES_LOG add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Тип операции из справочника «Типы операций»" (352000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'OPERATION_TYPE')) then
        execute 'alter table INSUR_CHANGES_LOG add OPERATION_TYPE VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'OPERATION_TYPE_CODE')) then
        execute 'alter table INSUR_CHANGES_LOG add OPERATION_TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий: причина изменения данных" (352000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'REASON')) then
        execute 'alter table INSUR_CHANGES_LOG add REASON VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор пользователя" (352000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'USER_ID')) then
        execute 'alter table INSUR_CHANGES_LOG add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Старое значение" (352000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'OLD_VALUE')) then
        execute 'alter table INSUR_CHANGES_LOG add OLD_VALUE VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Новое значение" (352000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_CHANGES_LOG', 'NEW_VALUE')) then
        execute 'alter table INSUR_CHANGES_LOG add NEW_VALUE VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (353000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ACTUAL_COST_RATIO', 'ID')) then
        execute 'alter table INSUR_ACTUAL_COST_RATIO add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала действия" (353000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ACTUAL_COST_RATIO', 'DATE_BEGIN')) then
        execute 'alter table INSUR_ACTUAL_COST_RATIO add DATE_BEGIN TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Значение коэффициента" (353000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_ACTUAL_COST_RATIO', 'VALUE')) then
        execute 'alter table INSUR_ACTUAL_COST_RATIO add VALUE NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер" (354000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'EMP_ID')) then
        execute 'alter table INSUR_REESTR_PAY add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра оплат" (354000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'NUM')) then
        execute 'alter table INSUR_REESTR_PAY add NUM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата реестра оплат" (354000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'DATE')) then
        execute 'alter table INSUR_REESTR_PAY add DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (354000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'DATA_CREATION')) then
        execute 'alter table INSUR_REESTR_PAY add DATA_CREATION TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата оплаты" (354000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'DATA_PAYMENT')) then
        execute 'alter table INSUR_REESTR_PAY add DATA_PAYMENT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, создавший реестр оплат" (354000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'USER_CREATION')) then
        execute 'alter table INSUR_REESTR_PAY add USER_CREATION VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, который ввел информацию об оплате" (354000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'USER_PAYMENT')) then
        execute 'alter table INSUR_REESTR_PAY add USER_PAYMENT VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Статус реестра оплат" (354000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'STATUS')) then
        execute 'alter table INSUR_REESTR_PAY add STATUS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'STATUS_CODE')) then
        execute 'alter table INSUR_REESTR_PAY add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип реестра оплат" (354000900)--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'TYPE_CODE')) then
        execute 'alter table INSUR_REESTR_PAY add TYPE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Примечание" (354001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'NOTE')) then
        execute 'alter table INSUR_REESTR_PAY add NOTE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл для статуса "Утвержден в ДГИ"" (354001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'FILE_STORAGE_ID_DGI')) then
        execute 'alter table INSUR_REESTR_PAY add FILE_STORAGE_ID_DGI INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на файл для статуса "Передано в оплату"" (354001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_REESTR_PAY', 'FILE_STORAGE_ID_PAY')) then
        execute 'alter table INSUR_REESTR_PAY add FILE_STORAGE_ID_PAY INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер" (355000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'EMP_ID')) then
        execute 'alter table INSUR_INVOICE add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Название субъекта –получателя выплаты ( ФИО/организация)" (355000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'SUBJECT_NAME')) then
        execute 'alter table INSUR_INVOICE add SUBJECT_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Телефон" (355000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'PHONE')) then
        execute 'alter table INSUR_INVOICE add PHONE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (355000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'DATA_INPUT')) then
        execute 'alter table INSUR_INVOICE add DATA_INPUT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ИНН" (355000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'INN')) then
        execute 'alter table INSUR_INVOICE add INN VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "КПП" (355000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'KPP')) then
        execute 'alter table INSUR_INVOICE add KPP VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Бик банка" (355000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'BIC_BANK')) then
        execute 'alter table INSUR_INVOICE add BIC_BANK VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Название банка" (355000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'BANK_NAME')) then
        execute 'alter table INSUR_INVOICE add BANK_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Корреспондентский счет" (355000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'KOR_ACC')) then
        execute 'alter table INSUR_INVOICE add KOR_ACC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Расчетный счет" (355001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'RACH_ACC')) then
        execute 'alter table INSUR_INVOICE add RACH_ACC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Лицевой счет" (355001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'LIC_ACC')) then
        execute 'alter table INSUR_INVOICE add LIC_ACC VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Номер банковской карточки" (355001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'NUM_CARD')) then
        execute 'alter table INSUR_INVOICE add NUM_CARD VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_DAMAGE ( Реестр дел по расчету ущербов)" (355001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'LINK_DAMAGE')) then
        execute 'alter table INSUR_INVOICE add LINK_DAMAGE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_ALL_PROPERTY (Реестр договоров по общему имуществу)" (355001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'LINK_ALL_PROPERTY')) then
        execute 'alter table INSUR_INVOICE add LINK_ALL_PROPERTY INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_REESTR_PAY ( Реестр оплат )" (355001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'LINK_REESTR_PAY')) then
        execute 'alter table INSUR_INVOICE add LINK_REESTR_PAY INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сумма выплаты для Реестра оплат доли города" (355001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'SUM_OPL')) then
        execute 'alter table INSUR_INVOICE add SUM_OPL NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на INSUR_FSP ( Реестр ФСП)" (355001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'LINK_FSP')) then
        execute 'alter table INSUR_INVOICE add LINK_FSP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус записи" (355001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'STATUS_NAME')) then
        execute 'alter table INSUR_INVOICE add STATUS_NAME VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_INVOICE', 'STATUS_CODE')) then
        execute 'alter table INSUR_INVOICE add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Причина отказа" (355001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'NOTE_NO_PAY_ID')) then
        execute 'alter table INSUR_INVOICE add NOTE_NO_PAY_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь, создавший счет" (355002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'USER_ID')) then
        execute 'alter table INSUR_INVOICE add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий" (355002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'COMMENT')) then
        execute 'alter table INSUR_INVOICE add COMMENT VARCHAR';
    end if;
end $$;


-- Перенос колонок показателя "ИНН банка" (355002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'INN_BANK')) then
        execute 'alter table INSUR_INVOICE add INN_BANK VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "КПП банка" (355002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'KPP_BANK')) then
        execute 'alter table INSUR_INVOICE add KPP_BANK VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата согласования" (355002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'DATE_AGREE')) then
        execute 'alter table INSUR_INVOICE add DATE_AGREE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь" (355002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'USER_AGREE_ID')) then
        execute 'alter table INSUR_INVOICE add USER_AGREE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер счета" (355002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'NUM_INVOICE')) then
        execute 'alter table INSUR_INVOICE add NUM_INVOICE VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Дата счета" (355002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'DATE_INVOICE')) then
        execute 'alter table INSUR_INVOICE add DATE_INVOICE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на субъект-получатель" (355002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'SUBJECT_ID')) then
        execute 'alter table INSUR_INVOICE add SUBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на банк" (355002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_INVOICE', 'BANK_ID')) then
        execute 'alter table INSUR_INVOICE add BANK_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (356000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'ID')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Причина ущерба по ЖП (справочник, 12125)" (356000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'CAUSES_OF_DAMAGE')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add CAUSES_OF_DAMAGE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'CAUSES_OF_DAMAGE_CODE')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add CAUSES_OF_DAMAGE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Подпричины ущерба по ЖП (справочник, 12134)" (356000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'SUBREASON_CAUSES_OF_DAMAGE')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add SUBREASON_CAUSES_OF_DAMAGE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'SUBREASON_CAUSES_OF_DAMAGE_CODE')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add SUBREASON_CAUSES_OF_DAMAGE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уточнение Подпричины ущерба (справочник, 12135)" (356000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'REFINEMENT_SUBREASON')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add REFINEMENT_SUBREASON VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_LINK_CAUSES_SUBREASON_LP', 'REFINEMENT_SUBREASON_CODE')) then
        execute 'alter table INSUR_LINK_CAUSES_SUBREASON_LP add REFINEMENT_SUBREASON_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (357000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_GBU_NO_PAY_REASON', 'ID')) then
        execute 'alter table INSUR_GBU_NO_PAY_REASON add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Причина" (357000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_GBU_NO_PAY_REASON', 'REASON')) then
        execute 'alter table INSUR_GBU_NO_PAY_REASON add REASON VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Вид страхования" (357000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_GBU_NO_PAY_REASON', 'TYPE_INSUR')) then
        execute 'alter table INSUR_GBU_NO_PAY_REASON add TYPE_INSUR VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Краткое пояснение (должно быть напечатано на заключении)" (357000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_GBU_NO_PAY_REASON', 'SHORT_EXPLANATION')) then
        execute 'alter table INSUR_GBU_NO_PAY_REASON add SHORT_EXPLANATION VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (358000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMENT', 'ID')) then
        execute 'alter table INSUR_COMMENT add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Комментарий" (358000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMENT', 'COMMENT')) then
        execute 'alter table INSUR_COMMENT add COMMENT VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Пользователь" (358000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMENT', 'USER_ID')) then
        execute 'alter table INSUR_COMMENT add USER_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата создания" (358000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMENT', 'DATE_CREATE')) then
        execute 'alter table INSUR_COMMENT add DATE_CREATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Ссылка на реестр дел" (358000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMENT', 'LINK_OBJECT_ID')) then
        execute 'alter table INSUR_COMMENT add LINK_OBJECT_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Номер реестра" (358000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_COMMENT', 'LINK_REESTR_ID')) then
        execute 'alter table INSUR_COMMENT add LINK_REESTR_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (359000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'EMP_ID')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор загруженного файла" (359000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'INPUT_FILE_ID')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add INPUT_FILE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество записей для обработки" (359000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'PLAT_COUNT')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add PLAT_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус" (359000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'STATUS')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add STATUS VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'STATUS_CODE')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала процесса идентифкации" (359000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'START_DATE')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата окончания процесса идентификации" (359000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'END_DATE')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add END_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Количество идентифицированных записей" (359000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'IDENTIFIED_COUNT')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add IDENTIFIED_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество неидентифицированных записей" (359000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PLAT_IDENTIFY_LOG', 'NOT_IDENTIFIED_COUNT')) then
        execute 'alter table INSUR_FILE_PLAT_IDENTIFY_LOG add NOT_IDENTIFIED_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Уникальный номер записи" (368000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'ID')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Тип здания (справочник "Типы зданий", 12132)" (368000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'TYPE_BUILDING')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add TYPE_BUILDING VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'TYPE_BUILDING_CODE')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add TYPE_BUILDING_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Тип конструкции строения (справочник "Тип конструкции строения", 12143)" (368000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'TYPE_BUILDING_STRUCTURE')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add TYPE_BUILDING_STRUCTURE VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'TYPE_BUILDING_STRUCTURE_CODE')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add TYPE_BUILDING_STRUCTURE_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Этажность строения (справочник "Этажность строения", 12144)" (368000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'TYPE_FLOORS')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add TYPE_FLOORS VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_TYPE_BUILDING_FLOOR_LINK', 'TYPE_FLOORS_CODE')) then
        execute 'alter table INSUR_TYPE_BUILDING_FLOOR_LINK add TYPE_FLOORS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (370000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'EMP_ID')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор файла для обработки" (370000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'INPUT_FILE_ID')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add INPUT_FILE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество записей для обработки" (370000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'TOTAL_COUNT')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add TOTAL_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Статус обработки" (370000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'STATUS')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add STATUS VARCHAR';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'STATUS_CODE')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add STATUS_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата начала процесса обработки" (370000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'START_DATE')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add START_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата окончания процесса обработки" (370000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'END_DATE')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add END_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Обработано записей" (370000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'PROCESSED_COUNT')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add PROCESSED_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество фсп для перерасчета" (370000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'TOTAL_FSP_COUNT')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add TOTAL_FSP_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Количество рассчитанных ФСП" (370000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('INSUR_FILE_PROCESS_LOG', 'PROCESSED_FSP_COUNT')) then
        execute 'alter table INSUR_FILE_PROCESS_LOG add PROCESSED_FSP_COUNT INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (400000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'EMP_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "ehd.building_parcel.id" (400000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'BUILDING_PARCEL_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add BUILDING_PARCEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.building_parcel.global_id" (400000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'GLOBAL_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add GLOBAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Наименование (ehd.building_parcel.name)" (400000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'NAME')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add NAME VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Код назначения (ehd.building_parcel.assignation_code)" (400000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'ASSIGNATION_CODE')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add ASSIGNATION_CODE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Площадь в квадратных метрах (ehd.building_parcel.area)" (400000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'AREA')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add AREA NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Примечание (ehd.building_parcel.notes)" (400000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'NOTES')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add NOTES VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Назначение" (400000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'ASSIGNATION_NAME')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add ASSIGNATION_NAME VARCHAR(255)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'ASSIGNATION_NAME_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add ASSIGNATION_NAME_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Степень готовности в процентах" (400000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'DEGREE_READINESS')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add DEGREE_READINESS NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "global_id последней версии данного объекта, null для последней версии" (400001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'ACTUAL_EHD')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add ACTUAL_EHD INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата данного обновления" (400001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'UPDATE_DATE_EHD')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add UPDATE_DATE_EHD TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Виды объектов государственного кадастра недвижимости (гкн) и единого государственного реестра прав на недвижимое имущество и сделок с ним (егрп)" (400001200)--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'TYPE_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add TYPE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Сведения о частях здания" (400001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'SUBBUILDINGS')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add SUBBUILDINGS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер объекта" (400001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'OBJECT_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add OBJECT_ID VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "ehd.building_parcel.package_id" (400001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'PACKAGE_ID')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add PACKAGE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Дата актуальности" (400001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'ACTUAL_ON_DATE')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add ACTUAL_ON_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (400001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'LOAD_DATE')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Кол-во этажей" (400001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'FLOORS')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add FLOORS INT2';
    end if;
end $$;


-- Перенос колонок показателя "Материал стен" (400001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'ELEMENTS_CONSTRUCT')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add ELEMENTS_CONSTRUCT VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "Условный номер" (400002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_BUILD_PARCEL_Q', 'OLD_NUMBER')) then
        execute 'alter table EHD_BUILD_PARCEL_Q add OLD_NUMBER VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (401000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'EMP_ID')) then
        execute 'alter table EHD_REGISTER_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (401000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'LOAD_DATE')) then
        execute 'alter table EHD_REGISTER_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ehd.register.BUILDING_PARCEL_ID" (401000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'BUILDING_PARCEL_ID')) then
        execute 'alter table EHD_REGISTER_Q add BUILDING_PARCEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.register.global_id" (401000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'GLOBAL_ID')) then
        execute 'alter table EHD_REGISTER_Q add GLOBAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер квартала (ehd.register.cadastral_number_parent)" (401000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'CADASTRAL_NUMBER_PARENT')) then
        execute 'alter table EHD_REGISTER_Q add CADASTRAL_NUMBER_PARENT VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер (ehd.register.cadastral_number)" (401000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'CADASTRAL_NUMBER')) then
        execute 'alter table EHD_REGISTER_Q add CADASTRAL_NUMBER VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Дата постановки на учет (ehd.register.date_created)" (401000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'DATE_CREATED')) then
        execute 'alter table EHD_REGISTER_Q add DATE_CREATED TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Дата снятия с учета (ehd.register.date_removed)" (401000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'DATE_REMOVED')) then
        execute 'alter table EHD_REGISTER_Q add DATE_REMOVED TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Статус объекта Росреестр" (401000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'STATE')) then
        execute 'alter table EHD_REGISTER_Q add STATE VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('EHD_REGISTER_Q', 'STATE_ID')) then
        execute 'alter table EHD_REGISTER_Q add STATE_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Способ образования объекта (ehd.register.method)" (401001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'METHOD')) then
        execute 'alter table EHD_REGISTER_Q add METHOD VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер здания или сооружения, в котором расположено помещение" (401001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'CADASTRAL_NUMBER_OKS')) then
        execute 'alter table EHD_REGISTER_Q add CADASTRAL_NUMBER_OKS VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер кк, в котором расположено помещение" (401001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'CADASTRAL_NUMBER_KK')) then
        execute 'alter table EHD_REGISTER_Q add CADASTRAL_NUMBER_KK VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Кадастровый номер квартиры, в которой расположена комната" (401001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'CADASTRAL_NUMBER_FLAT')) then
        execute 'alter table EHD_REGISTER_Q add CADASTRAL_NUMBER_FLAT VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Нежилое помещение, являющееся общим имуществом в многоквартирном доме" (401001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'TOTALASS')) then
        execute 'alter table EHD_REGISTER_Q add TOTALASS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Классификатор назначений помещений" (401001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'ASSFTP1')) then
        execute 'alter table EHD_REGISTER_Q add ASSFTP1 VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('EHD_REGISTER_Q', 'ASSFTP1_CODE')) then
        execute 'alter table EHD_REGISTER_Q add ASSFTP1_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Код назначения помещения" (401001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_REGISTER_Q', 'ASSFTP_CD')) then
        execute 'alter table EHD_REGISTER_Q add ASSFTP_CD VARCHAR(4000)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('EHD_REGISTER_Q', 'ASSFTP_CD_CODE')) then
        execute 'alter table EHD_REGISTER_Q add ASSFTP_CD_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (402000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'EMP_ID')) then
        execute 'alter table EHD_LOCATION_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (402000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'LOAD_DATE')) then
        execute 'alter table EHD_LOCATION_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.id" (402000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'ID_LOCATION_EHD')) then
        execute 'alter table EHD_LOCATION_Q add ID_LOCATION_EHD INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.parcel_id" (402000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'PARCEL_ID')) then
        execute 'alter table EHD_LOCATION_Q add PARCEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.person_id" (402000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'PERSON_ID')) then
        execute 'alter table EHD_LOCATION_Q add PERSON_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.organization_id" (402000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'ORGANIZATION_ID')) then
        execute 'alter table EHD_LOCATION_Q add ORGANIZATION_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.building_parcel_id" (402000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'BUILDING_PARCEL_ID')) then
        execute 'alter table EHD_LOCATION_Q add BUILDING_PARCEL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.global_id" (402000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'GLOBAL_ID')) then
        execute 'alter table EHD_LOCATION_Q add GLOBAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "Положение на дкк" (402000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'PLACED')) then
        execute 'alter table EHD_LOCATION_Q add PLACED VARCHAR(5)';
    end if;
end $$;


-- Перенос колонок показателя "В границах" (402001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'IN_BOUNDS')) then
        execute 'alter table EHD_LOCATION_Q add IN_BOUNDS VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Код окато" (402001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'CODE_OKATO')) then
        execute 'alter table EHD_LOCATION_Q add CODE_OKATO VARCHAR(11)';
    end if;
end $$;


-- Перенос колонок показателя "Код кладр" (402001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'CODE_KLADR')) then
        execute 'alter table EHD_LOCATION_Q add CODE_KLADR VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "Почтовый индекс" (402001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'POSTAL_CODE')) then
        execute 'alter table EHD_LOCATION_Q add POSTAL_CODE VARCHAR(6)';
    end if;
end $$;


-- Перенос колонок показателя "Код региона" (402001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'REGION')) then
        execute 'alter table EHD_LOCATION_Q add REGION VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "Наименование района" (402001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'DISTRICT')) then
        execute 'alter table EHD_LOCATION_Q add DISTRICT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Муниципальное образование" (402001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'CITY')) then
        execute 'alter table EHD_LOCATION_Q add CITY VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Городской район" (402001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'URBAN_DISTRICT')) then
        execute 'alter table EHD_LOCATION_Q add URBAN_DISTRICT VARCHAR(306)';
    end if;
end $$;


-- Перенос колонок показателя "Сельсовет" (402001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'SOVIET_VILLAGE')) then
        execute 'alter table EHD_LOCATION_Q add SOVIET_VILLAGE VARCHAR(266)';
    end if;
end $$;


-- Перенос колонок показателя "Населенный пункт" (402001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'LOCALITY')) then
        execute 'alter table EHD_LOCATION_Q add LOCALITY VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Улица" (402002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'STREET')) then
        execute 'alter table EHD_LOCATION_Q add STREET VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Дом" (402002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'LEVEL1')) then
        execute 'alter table EHD_LOCATION_Q add LEVEL1 VARCHAR(306)';
    end if;
end $$;


-- Перенос колонок показателя "Корпус" (402002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'LEVEL2')) then
        execute 'alter table EHD_LOCATION_Q add LEVEL2 VARCHAR(306)';
    end if;
end $$;


-- Перенос колонок показателя "Строение" (402002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'LEVEL3')) then
        execute 'alter table EHD_LOCATION_Q add LEVEL3 VARCHAR(306)';
    end if;
end $$;


-- Перенос колонок показателя "Квартира" (402002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'APARTMENT')) then
        execute 'alter table EHD_LOCATION_Q add APARTMENT VARCHAR(306)';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.full_address" (402002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'FULL_ADDRESS')) then
        execute 'alter table EHD_LOCATION_Q add FULL_ADDRESS VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "Полный адрес" (402002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'ADDRESS_TOTAL')) then
        execute 'alter table EHD_LOCATION_Q add ADDRESS_TOTAL VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "ehd.location.other" (402002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_LOCATION_Q', 'OTHER')) then
        execute 'alter table EHD_LOCATION_Q add OTHER VARCHAR(2500)';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (405000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'EMP_ID')) then
        execute 'alter table EHD_EGRP_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "id- записи в EHD.EGRP (связка с RIGHT_FROM_EHD)" (405000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ID_IN_EHD_EGRP')) then
        execute 'alter table EHD_EGRP_Q add ID_IN_EHD_EGRP INT8';
    end if;
end $$;


-- Перенос колонок показателя "Площадь  объекта ЕГРП" (405000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'AREA')) then
        execute 'alter table EHD_EGRP_Q add AREA NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "Одноименное поле в EHD.EGRP" (405000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'GLOBAL_ID')) then
        execute 'alter table EHD_EGRP_Q add GLOBAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "вид объекта недвижимости" (405000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'OBJT_CD')) then
        execute 'alter table EHD_EGRP_Q add OBJT_CD VARCHAR(40)';
    end if;
end $$;


-- Перенос колонок показателя "тип объекта недвижимости" (405000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'OBJECTTP_CD')) then
        execute 'alter table EHD_EGRP_Q add OBJECTTP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип региона" (405000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'REGTP_CD')) then
        execute 'alter table EHD_EGRP_Q add REGTP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип района" (405000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'DISTTP_CD')) then
        execute 'alter table EHD_EGRP_Q add DISTTP_CD VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "тип города" (405000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'CITYTP_CD')) then
        execute 'alter table EHD_EGRP_Q add CITYTP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип населённых пунктов" (405001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'LOCTP_CD')) then
        execute 'alter table EHD_EGRP_Q add LOCTP_CD VARCHAR(40)';
    end if;
end $$;


-- Перенос колонок показателя "тип улицы" (405001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'STRTP_CD')) then
        execute 'alter table EHD_EGRP_Q add STRTP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип дома" (405001200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'LEVEL1TP_CD')) then
        execute 'alter table EHD_EGRP_Q add LEVEL1TP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип корпуса" (405001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'LEVEL2TP_CD')) then
        execute 'alter table EHD_EGRP_Q add LEVEL2TP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип строения" (405001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'LEVEL3TP_CD')) then
        execute 'alter table EHD_EGRP_Q add LEVEL3TP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "тип квартиры" (405001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'APARTTP_CD')) then
        execute 'alter table EHD_EGRP_Q add APARTTP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "код назначения объекта недвижимости" (405001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'PURPOSETP_CD')) then
        execute 'alter table EHD_EGRP_Q add PURPOSETP_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "статус состояния записи объекта" (405001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'OBJECTST_CD')) then
        execute 'alter table EHD_EGRP_Q add OBJECTST_CD VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "статус актуальности информации об объекте" (405001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ACTST_CD')) then
        execute 'alter table EHD_EGRP_Q add ACTST_CD VARCHAR(20)';
    end if;
end $$;


-- Перенос колонок показателя "код вида использования земель (фактический)" (405001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'FAKT_CD')) then
        execute 'alter table EHD_EGRP_Q add FAKT_CD VARCHAR(511)';
    end if;
end $$;


-- Перенос колонок показателя "код вида использования земель (по документам)" (405002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'BYDOC_CD')) then
        execute 'alter table EHD_EGRP_Q add BYDOC_CD VARCHAR(511)';
    end if;
end $$;


-- Перенос колонок показателя "целевое назначение земли (категория)" (405002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'GROUNDCAT_CD')) then
        execute 'alter table EHD_EGRP_Q add GROUNDCAT_CD VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "назначение объекта недвижимости" (405002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'PURPOSE')) then
        execute 'alter table EHD_EGRP_Q add PURPOSE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "инвентарный номер" (405002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'INVNUM')) then
        execute 'alter table EHD_EGRP_Q add INVNUM VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "литер бти" (405002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'LITERBTI')) then
        execute 'alter table EHD_EGRP_Q add LITERBTI VARCHAR(60)';
    end if;
end $$;


-- Перенос колонок показателя "уточнение местоположения (ориентира)" (405002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_REFMARK')) then
        execute 'alter table EHD_EGRP_Q add ADDR_REFMARK VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "уникальный идентификатор адреса" (405002600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_ID')) then
        execute 'alter table EHD_EGRP_Q add ADDR_ID VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "код страны по классификатору оксм" (405002700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_CDCOUNTRY')) then
        execute 'alter table EHD_EGRP_Q add ADDR_CDCOUNTRY VARCHAR(3)';
    end if;
end $$;


-- Перенос колонок показателя "окато" (405002800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_CDOKATO')) then
        execute 'alter table EHD_EGRP_Q add ADDR_CDOKATO VARCHAR(11)';
    end if;
end $$;


-- Перенос колонок показателя "почтовый индекс" (405002900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_POSTCD')) then
        execute 'alter table EHD_EGRP_Q add ADDR_POSTCD VARCHAR(6)';
    end if;
end $$;


-- Перенос колонок показателя "наименование района" (405003000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_DIST_NAME')) then
        execute 'alter table EHD_EGRP_Q add ADDR_DIST_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "район. код кладр адресного объекта" (405003100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_DIST_CD')) then
        execute 'alter table EHD_EGRP_Q add ADDR_DIST_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "наименование города" (405003200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_CITY_NAME')) then
        execute 'alter table EHD_EGRP_Q add ADDR_CITY_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "город. код кладр адресного объекта" (405003300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_CITY_CD')) then
        execute 'alter table EHD_EGRP_Q add ADDR_CITY_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "наименование населенного пункта" (405003400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_LOC_NAME')) then
        execute 'alter table EHD_EGRP_Q add ADDR_LOC_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "населенный пункт. код кладр адресного объекта" (405003500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_LOC_CD')) then
        execute 'alter table EHD_EGRP_Q add ADDR_LOC_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "наименование улицы" (405003600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_STR_NAME')) then
        execute 'alter table EHD_EGRP_Q add ADDR_STR_NAME VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "улица. код кладр адресного объекта" (405003700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_STR_CD')) then
        execute 'alter table EHD_EGRP_Q add ADDR_STR_CD VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "номер дома" (405003800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_LEVEL1_NUM')) then
        execute 'alter table EHD_EGRP_Q add ADDR_LEVEL1_NUM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "номер корпуса" (405003900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_LEVEL2_NUM')) then
        execute 'alter table EHD_EGRP_Q add ADDR_LEVEL2_NUM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "номер строения" (405004000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_LEVEL3_NUM')) then
        execute 'alter table EHD_EGRP_Q add ADDR_LEVEL3_NUM VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "номер квартиры" (405004100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_APART')) then
        execute 'alter table EHD_EGRP_Q add ADDR_APART VARCHAR(255)';
    end if;
end $$;


-- Перенос колонок показателя "адрес. иное" (405004200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_OTHER')) then
        execute 'alter table EHD_EGRP_Q add ADDR_OTHER VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "адрес. неформализованное описание" (405004300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDR_NOTE')) then
        execute 'alter table EHD_EGRP_Q add ADDR_NOTE VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "кадастровый номер ( связка с BULDING_PARCEL_FROM_EHD)" (405004400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'NUM_CADNUM')) then
        execute 'alter table EHD_EGRP_Q add NUM_CADNUM VARCHAR(400)';
    end if;
end $$;


-- Перенос колонок показателя "условный номер" (405004500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'NUM_CONDNUM')) then
        execute 'alter table EHD_EGRP_Q add NUM_CONDNUM VARCHAR(3000)';
    end if;
end $$;


-- Перенос колонок показателя "наименование объекта недвижимости" (405004600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'NAME')) then
        execute 'alter table EHD_EGRP_Q add NAME VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "этажность (для зданий)" (405004700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'FLOOR_GR')) then
        execute 'alter table EHD_EGRP_Q add FLOOR_GR VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "этажность подземная (для зданий)" (405004800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'FLOOR_UND')) then
        execute 'alter table EHD_EGRP_Q add FLOOR_UND VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "высота" (405004900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'TECHAR_HEIGHT')) then
        execute 'alter table EHD_EGRP_Q add TECHAR_HEIGHT VARCHAR(22)';
    end if;
end $$;


-- Перенос колонок показателя "длина" (405005000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'TECHAR_LENGHT')) then
        execute 'alter table EHD_EGRP_Q add TECHAR_LENGHT VARCHAR(22)';
    end if;
end $$;


-- Перенос колонок показателя "объем" (405005100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'TECHAR_VOL')) then
        execute 'alter table EHD_EGRP_Q add TECHAR_VOL VARCHAR(22)';
    end if;
end $$;


-- Перенос колонок показателя "номер этажа (для помещений)" (405005200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'NUM_FLOOR')) then
        execute 'alter table EHD_EGRP_Q add NUM_FLOOR VARCHAR(200)';
    end if;
end $$;


-- Перенос колонок показателя "номер помещения" (405005300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'NUM_FLAT')) then
        execute 'alter table EHD_EGRP_Q add NUM_FLAT VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "дата регистрации объекта (открытия подраздела i егрп)" (405005400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'REGDT')) then
        execute 'alter table EHD_EGRP_Q add REGDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "дата ликвидации, изменения объекта (закрытия подраздела i егрп)" (405005500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'BRKDT')) then
        execute 'alter table EHD_EGRP_Q add BRKDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "дата внесения изменений" (405005600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'MDFDT')) then
        execute 'alter table EHD_EGRP_Q add MDFDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "дата изменения информации" (405005700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'UPDT')) then
        execute 'alter table EHD_EGRP_Q add UPDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "дата, начиная с которой объект перестал быть актуальным" (405005800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ACT_DT')) then
        execute 'alter table EHD_EGRP_Q add ACT_DT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "уникальный идентификатор объекта недвижимости в учетной системе" (405005900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'OBJECT_ID')) then
        execute 'alter table EHD_EGRP_Q add OBJECT_ID VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "дата данного обновления" (405006000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'UPDATE_DATE')) then
        execute 'alter table EHD_EGRP_Q add UPDATE_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "global_id последней версии данного участка, null для последней версии" (405006100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ACTUAL_ID')) then
        execute 'alter table EHD_EGRP_Q add ACTUAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "актуальность записи" (405006200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ACTUAL_ON_DATE')) then
        execute 'alter table EHD_EGRP_Q add ACTUAL_ON_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "адрес (объединение всех полей)" (405006300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'ADDRESS_TOTAL')) then
        execute 'alter table EHD_EGRP_Q add ADDRESS_TOTAL VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "json представление данного документа" (405006400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'JSON')) then
        execute 'alter table EHD_EGRP_Q add JSON VARCHAR(100)';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (405006500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_EGRP_Q', 'LOAD_DATE')) then
        execute 'alter table EHD_EGRP_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "Идентификатор" (406000100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'EMP_ID')) then
        execute 'alter table EHD_RIGHT_Q add EMP_ID INT8 not null';
    end if;
end $$;


-- Перенос колонок показателя "id_in ehd.egrp ( ссылка на ID - EGRP  в  Реестре EGRP_FROM_EHD)" (406000200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'EGRP_ID')) then
        execute 'alter table EHD_RIGHT_Q add EGRP_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "global_id" (406000300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'GLOBAL_ID')) then
        execute 'alter table EHD_RIGHT_Q add GLOBAL_ID INT8';
    end if;
end $$;


-- Перенос колонок показателя "id  в EHD.RIGHT" (406000400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'ID_EHD_RIGHT')) then
        execute 'alter table EHD_RIGHT_Q add ID_EHD_RIGHT INT8';
    end if;
end $$;


-- Перенос колонок показателя "дата внесения изменений" (406000500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'MDFDT')) then
        execute 'alter table EHD_RIGHT_Q add MDFDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "код егрп" (406000600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'OBJECT_ID')) then
        execute 'alter table EHD_RIGHT_Q add OBJECT_ID VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "регистрация действия. прекращение (дата государственной регистрации)" (406000700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'REG_CLOSE_REGDT')) then
        execute 'alter table EHD_RIGHT_Q add REG_CLOSE_REGDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "регистрация действия. прекращение (номер государственной регистрации)" (406000800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'REG_CLOSE_REGNUM')) then
        execute 'alter table EHD_RIGHT_Q add REG_CLOSE_REGNUM VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "регистрация действия. возникновение (дата государственной регистрации)" (406000900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'REG_OPEN_REGDT')) then
        execute 'alter table EHD_RIGHT_Q add REG_OPEN_REGDT TIMESTAMP';
    end if;
end $$;


-- Перенос колонок показателя "регистрация действия. возникновение (номер государственной регистрации)" (406001000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'REG_OPEN_REGNUM')) then
        execute 'alter table EHD_RIGHT_Q add REG_OPEN_REGNUM VARCHAR(50)';
    end if;
end $$;


-- Перенос колонок показателя "статус состояния записи права" (406001100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'RIGHTST_CD')) then
        execute 'alter table EHD_RIGHT_Q add RIGHTST_CD VARCHAR(14)';
    end if;
end $$;


-- Перенос колонок показателя "вид права" (406001300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'RIGHTTP_CD')) then
        execute 'alter table EHD_RIGHT_Q add RIGHTTP_CD VARCHAR(50)';
    end if;
end $$;
--<DO>--
-- Перенос колонки кода
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not CORE_updstru_CheckExistColumn('EHD_RIGHT_Q', 'RIGHTTP_CD_CODE')) then
        execute 'alter table EHD_RIGHT_Q add RIGHTTP_CD_CODE INT8';
    end if;
end $$;


-- Перенос колонок показателя "код права" (406001400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'RIGHT_KEY')) then
        execute 'alter table EHD_RIGHT_Q add RIGHT_KEY VARCHAR(30)';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве общей долевой собственности на общее имущество в квартире (знаменатель дроби)" (406001500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARECOMFLAT_DEN')) then
        execute 'alter table EHD_RIGHT_Q add SHARECOMFLAT_DEN NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве общей долевой собственности на общее имущество в квартире (числитель дроби)" (406001600)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARECOMFLAT_NUM')) then
        execute 'alter table EHD_RIGHT_Q add SHARECOMFLAT_NUM NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве общей долевой собственности на общее имущество в квартире (значение текстом)" (406001700)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARECOMFLAT_TEXT')) then
        execute 'alter table EHD_RIGHT_Q add SHARECOMFLAT_TEXT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве общей долевой собственности на общее имущество в многоквартирном доме (знаменатель дроби)" (406001800)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARECOM_DEN')) then
        execute 'alter table EHD_RIGHT_Q add SHARECOM_DEN NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве общей долевой собственности на общее имущество в многоквартирном доме (числитель дроби)" (406001900)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARECOM_NUM')) then
        execute 'alter table EHD_RIGHT_Q add SHARECOM_NUM NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве общей долевой собственности на общее имущество в многоквартирном доме (значение текстом)" (406002000)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARECOM_TEXT')) then
        execute 'alter table EHD_RIGHT_Q add SHARECOM_TEXT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве (знаменатель дроби)" (406002100)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARE_DEN')) then
        execute 'alter table EHD_RIGHT_Q add SHARE_DEN NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве (числитель дроби)" (406002200)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARE_NUM')) then
        execute 'alter table EHD_RIGHT_Q add SHARE_NUM NUMERIC';
    end if;
end $$;


-- Перенос колонок показателя "размер доли в праве (значение текстом)" (406002300)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'SHARE_TEXT')) then
        execute 'alter table EHD_RIGHT_Q add SHARE_TEXT VARCHAR(4000)';
    end if;
end $$;


-- Перенос колонок показателя "вид права" (406002400)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'TP_NAME')) then
        execute 'alter table EHD_RIGHT_Q add TP_NAME VARCHAR(400)';
    end if;
end $$;


-- Перенос колонок показателя "Дата загрузки" (406002500)--<DO>--
-- Перенос колонки значения
DO $$
begin
    -- Если колонка отсутствует в QUANT, то добавить
    if (not core_updstru_CheckExistColumn('EHD_RIGHT_Q', 'LOAD_DATE')) then
        execute 'alter table EHD_RIGHT_Q add LOAD_DATE TIMESTAMP';
    end if;
end $$;


