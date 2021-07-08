drop function if exists create_source_register;
create or replace function
    create_source_register(register_code int, register_description text,
                                  partitioning_type int = 1) returns void as
$BODY$
begin
    insert into core_register
    values (register_code, concat('Gbu.Source',register_code), concat('Источник: ',register_description),
            concat('GBU_SOURCE',register_code,'_A'), null, 'GBU_MAIN_OBJECT', null, 5, 'REG_OBJECT_SEQ', 0, 0, null, null,
            null, 0, partitioning_type, 200);
    insert into core_register_attribute (id,name,registerid,type,value_field,is_deleted,change_date,hidden,primary_key)
    values (register_code*100000+100, 'Идентификатор', register_code, 1, 'ID', 0, now(),0,1);
end
$BODY$
    LANGUAGE 'plpgsql';

drop function if exists create_source_register_attribute;
create or replace function
    create_source_register_attribute(attribute_id int, description text, register_id int, type int) returns void as
$BODY$
begin
    insert into core_register_attribute (id,name,registerid,type,is_deleted,change_date,hidden)
    values (attribute_id, description, register_id, type, 0, now(),0);
end
$BODY$
    LANGUAGE 'plpgsql';

drop function if exists create_source_register_table_for_datatype_partitioning();
create or replace function
    create_source_register_table_for_datatype_partitioning(register_id int, datatype text) returns void as
$BODY$
declare
    reg core_register%rowtype;
    script text;
begin
    select *
    from core_register
    where registerid = register_id
    into reg;

    script := format('create table gbu_source%1$s_a_%2$s
(
    id             bigint    not null
        constraint reg_%1$s_a_%2$s_pk
            primary key,
    object_id      bigint    not null
        constraint reg_%1$s_a_%2$s_fk_o
            references gbu_main_object,
    attribute_id   bigint    not null,
    ot             timestamp not null,
    s              timestamp not null,
    ',register_id,datatype);

    if datatype='txt'
    then script := concat(script,
        'ref_item_id    bigint,
         value varchar(5000),');
    elsif datatype='num'
    then script := concat(script, 'value numeric,');
    elsif datatype='dt'
    then script := concat(script, 'value timestamp,');
    else raise 'Неизвестный тип данных';
    end if;

    script := concat(script, '
    change_id      bigint,
    change_date    timestamp not null,
    change_user_id bigint    not null,
    change_doc_id  bigint)');

    execute script;
    execute format('create index reg_%1$s_a_%2$s_inx_obj_attr_id
    on gbu_source%1$s_a_%2$s (object_id, attribute_id);',register_id,datatype);
end
$BODY$
    LANGUAGE 'plpgsql';



drop function if exists create_source_register_table_for_attribute_partitioning();
create or replace function
    create_source_register_table_for_attribute_partitioning(attribute_id bigint) returns void as
$BODY$
declare
    attr core_register_attribute%rowtype;
    script text;
begin
    select *
    from core_register_attribute
    where id = attribute_id
    into attr;

    if attr.type = 1 then return;
    end if;

    script := format('create table gbu_source%1$s_a_%2$s
(
    id             bigint    not null
        constraint reg_%1$s_a_%2$s__pk
            primary key,
    object_id      bigint    not null
        constraint reg_%1$s_a_%2$s_fk_o
            references gbu_main_object,
    ot             timestamp not null,
    s              timestamp not null,
    ',attr.registerid,attr.id);

    if attr.type=2
    then script := concat(script,'value          numeric,');
    elsif attr.type=3
    then script := concat(script,
        'ref_item_id    bigint,
         value smallint,');
    elsif attr.type=4
    then script := concat(script,
        'ref_item_id    bigint,
         value varchar(5000),');
    elsif attr.type=5
    then script := concat(script, 'value timestamp,');
    else raise 'Неизвестный тип данных';
    end if;

    script := concat(script, '
    change_date    timestamp not null,
    change_user_id bigint    not null,
    change_doc_id  bigint)');

    execute script;
    execute format('create index reg_%1$s_a_%2$s_inx_obj_attr_id
    on gbu_source%1$s_a_%2$s (object_id, ot);',attr.registerid,attr.id);
end
$BODY$
    LANGUAGE 'plpgsql';


drop function if exists create_source_register_tables_from_records;
create or replace function
    create_source_register_tables_from_records(register_id int) returns void as
$BODY$
declare
    reg core_register%rowtype;
    attr core_register_attribute%rowtype;
begin
    select *
    from core_register
    where registerid = register_id
    into reg;

    if reg.allpri_partitioning = 1
    then
        perform create_source_register_table_for_datatype_partitioning(register_id,'txt');
        perform create_source_register_table_for_datatype_partitioning(register_id,'num');
        perform create_source_register_table_for_datatype_partitioning(register_id,'dt');
    end if;

    if reg.allpri_partitioning = 2
    then
        select *
        from core_register_attribute
        where registerid = register_id
        into attr;
        for attr in execute format('select * from core_register_attribute where registerid=%s',attr.registerid) loop
            perform create_source_register_table_for_attribute_partitioning(attr.id);
        end loop;
    end if;
end
$BODY$
    LANGUAGE 'plpgsql';

-- datatypes
-- 5 - dt
-- 4 - str
-- 3 - bool?
-- 2 - numeric
-- 1 - id

--наполнение данными
DO $$
BEGIN
perform create_source_register(3,'База данных МОБТИ');
perform create_source_register(4,'База данных предыдущего расчета');
perform create_source_register(5,'Графический расчет / Сведения ОМС');
perform create_source_register(2,'ЕГРН',2);
perform create_source_register(6,'Идентификатор для загрузки из Excel');
perform create_source_register(7,'Используется подсистемой моделирования');
perform create_source_register(8,'Источник информации о СЗЗ ТБО');
perform create_source_register(9,'Картографический расчет');
perform create_source_register(10,'Минсельхоз');
perform create_source_register(11,'Мособлстат');
perform create_source_register(12,'Определяется по результатам анализа изменений сведений ЕГРН');
perform create_source_register(13,'Определяется по результатам расчета стоимости');
perform create_source_register(14,'Пользовательский фактор');
perform create_source_register(15,'Предоставлен ОМС');
perform create_source_register(16,'Предыдущий расчет объекта');
perform create_source_register(17,'Проставляется из справочников кодировки');
perform create_source_register(18,'Расчетное значение');
perform create_source_register(19,'РГИС');
perform create_source_register(20,'Рыночная информация');
perform create_source_register(21,'Сведения о ЗУ');
perform create_source_register(22,'Соответствие с классификатором МЭР N 540');
perform create_source_register(23,'Ссылка на предыдущее поступление объекта');

perform create_source_register_attribute(1,'Наименование земельного участка',2,4);
perform create_source_register_attribute(2,'Площадь',2,2);
perform create_source_register_attribute(3,'Категория земель',2,4);
perform create_source_register_attribute(4,'Вид использования по документам',2,4);
perform create_source_register_attribute(5,'Вид использования по классификатору',2,4);
perform create_source_register_attribute(6,'Кадастровая стоимость',2,2);
perform create_source_register_attribute(8,'Местоположение',2,4);
perform create_source_register_attribute(13,'Дата образования',2,5);
perform create_source_register_attribute(14,'Назначение здания',2,4);
perform create_source_register_attribute(15,'Год постройки',2,4);
perform create_source_register_attribute(16,'Год ввода в эксплуатацию',2,4);
perform create_source_register_attribute(17,'Количество этажей',2,4);
perform create_source_register_attribute(18,'Количество подземных этажей',2,4);
perform create_source_register_attribute(19,'Наименование объекта',2,4);
perform create_source_register_attribute(20,'Оценочная группа',2,4);
perform create_source_register_attribute(21,'Материал стен',2,4);
perform create_source_register_attribute(22,'Назначение сооружения',2,4);
perform create_source_register_attribute(23,'Назначение помещения',2,4);
perform create_source_register_attribute(24,'Номер этажа',2,4);
perform create_source_register_attribute(25,'Тип этажа',2,4);
perform create_source_register_attribute(26,'Тип объекта',2,4);
perform create_source_register_attribute(27,'УПКС',2,2);
perform create_source_register_attribute(43,'Дата прекращения',2,5);
perform create_source_register_attribute(44,'Характеристика сооружения',2,4);
perform create_source_register_attribute(45,'Этаж',2,4);
perform create_source_register_attribute(46,'Процент готовности',2,2);
perform create_source_register_attribute(600,'Адрес',2,4);
perform create_source_register_attribute(601,'Кадастровый квартал',2,4);
perform create_source_register_attribute(602,'Земельный участок',2,4);
perform create_source_register_attribute(603,'Тип помещения',2,4);
perform create_source_register_attribute(604,'Кадастровый номер здания или сооружения, в котором расположено помещение',2,4);
perform create_source_register_attribute(605,'Кадастровый номер квартиры, в которой расположена комната',2,4);
--perform create_source_register_attribute(606,'Номер на плане',2,4);
perform create_source_register_attribute(660,'П1. Группа',2,3);
perform create_source_register_attribute(661,'П2. ФС',2,3);
perform create_source_register_attribute(662,'П3. Материал стен',2,3);
perform create_source_register_attribute(663,'П4. Год постройки',2,3);

perform create_source_register_attribute(1001,'Год постройки_МОБТИ_2015',3,4);
perform create_source_register_attribute(1002,'Год постройки_МОБТИ_2018',3,4);
perform create_source_register_attribute(1003,'Инвентарный номер_МОБТИ',3,4);
perform create_source_register_attribute(1004,'Материал стен_МОБТИ_2015',3,4);
perform create_source_register_attribute(1005,'Материал стен_МОБТИ_2018',3,4);
perform create_source_register_attribute(1006,'Строительный объем_МОБТИ',3,2);
perform create_source_register_attribute(1007,'Источник данных',4,4);
perform create_source_register_attribute(1008,'Попадание объекта в санитарную зону ТБО',5,4);
perform create_source_register_attribute(1009,'В границах',2,4);
--perform create_source_register_attribute(1010,'Вид (виды) разрешенного использования',2,4);
perform create_source_register_attribute(1011,'Вид жилого помещения',2,4);
perform create_source_register_attribute(1012,'Вид использования участка по документу',2,4);
perform create_source_register_attribute(1013,'Вид объекта культурного наследия РР',2,4);
perform create_source_register_attribute(1014,'Вид объекта недвижимости',2,4);
perform create_source_register_attribute(1015,'Вид объекта недвижимости, в котором расположено помещение',2,4);
perform create_source_register_attribute(1016,'Вид разрешенного использования по классификатору N 540',2,4);
perform create_source_register_attribute(1017,'Вид разрешенного использования по классификатору Росреестра',2,4);
perform create_source_register_attribute(1018,'Высота',2,2);
perform create_source_register_attribute(1019,'Газоснабжение',2,4);
perform create_source_register_attribute(1020,'Глубина',2,2);
perform create_source_register_attribute(1021,'Глубина залегания',2,2);
--perform create_source_register_attribute(1022,'Год ввода в эксплуатацию ',2,4);
perform create_source_register_attribute(1023,'Год ввода в эксплуатацию родительского здания (сооружения)',2,4);
-- perform create_source_register_attribute(1024,'Год постройки',2,4);
perform create_source_register_attribute(1025,'Год постройки родительского здания (сооружения)',2,4);
perform create_source_register_attribute(1026,'Город',2,4);
--perform create_source_register_attribute(1027,'Дата акта об утверждении кадастровой стоимости',2,4);
perform create_source_register_attribute(1028,'Дата внесения сведений о действующей кадастровой стоимости  в ГКН',2,4);
perform create_source_register_attribute(1029,'Дата вступления в законную силу акта (дата утверждения кадастровой стоимости)',2,4);
perform create_source_register_attribute(1030,'Дата инвентаризации',2,5);
--perform create_source_register_attribute(1031,'Дата начала применения кадастровой стоимости',2,5);
perform create_source_register_attribute(1032,'Дата определения кадастровой стоимости действующей',2,4);
perform create_source_register_attribute(1033,'Дата определения стоимости',2,5);
perform create_source_register_attribute(1034,'Дата отчета',2,5);
perform create_source_register_attribute(1035,'Дата постановки на учет (дата внесения кадастрового номера в ГКН)',2,4);
perform create_source_register_attribute(1036,'Дата установления стоимости',2,5);
perform create_source_register_attribute(1037,'Действующий удельный показатель  кадастровой стоимости, руб./кв. м',2,2);
perform create_source_register_attribute(1038,'Действующий удельный показатель кадастровой стоимости',2,2);
perform create_source_register_attribute(1039,'Длина',2,2);
--perform create_source_register_attribute(1040,'Дом',2,4);
perform create_source_register_attribute(1041,'Дополнительные сведения РР',2,4);
perform create_source_register_attribute(1042,'Жилая площадь',2,2);
perform create_source_register_attribute(1043,'Идентификационный номер ранее не учтенного в ЕГРОКС объекта',2,4);
perform create_source_register_attribute(1044,'Идентификационный номер учтенного ранее в ЕГРОКС объекта',2,4);
perform create_source_register_attribute(1045,'Инвентаризационная стоимость',2,2);
perform create_source_register_attribute(1046,'Инвентарный номер',2,4);
--perform create_source_register_attribute(1047,'Иное',2,4);
perform create_source_register_attribute(1048,'Исходный ОН   (РР)',2,4);
perform create_source_register_attribute(1049,'Кадастровая стоимость действующая',2,2);
--perform create_source_register_attribute(1050,'Кадастровые номера машино-мест, расположенных в объекте недвижимости',2,4);
--perform create_source_register_attribute(1051,'Кадастровые номера объектов недвижимости, расположенных в пределах земельного участка',2,4);
--perform create_source_register_attribute(1052,'Кадастровые номера помещений, расположенных в объекте недвижимости',2,4);
-- perform create_source_register_attribute(1053,'Кадастровый квартал',2,4);
--perform create_source_register_attribute(1054,'Кадастровый номер',2,4);
perform create_source_register_attribute(1055,'Кадастровый номер ЕКН, если объект недвижимости входит в состав ЕКН',2,4);
perform create_source_register_attribute(1056,'Кадастровый номер здания или сооружения, в которых расположено помещение',2,4);
perform create_source_register_attribute(1057,'Кадастровый номер земельного участка',2,4);
perform create_source_register_attribute(1058,'Кадастровый номер и назначение предприятия как имущественного комплекса',2,4);
perform create_source_register_attribute(1059,'Кадастровый номер квартиры',2,4);
perform create_source_register_attribute(1060,'Канализация',2,4);
-- perform create_source_register_attribute(1061,'Категория земель',2,4);
--perform create_source_register_attribute(1062,'Код КЛАДР',2,4);
perform create_source_register_attribute(1063,'Код обременения',2,4);
--perform create_source_register_attribute(1064,'Код ОКАТО',2,4);
perform create_source_register_attribute(1065,'Код ОКТМО',2,4);
perform create_source_register_attribute(1066,'Код ОКТМО_xml',2,4);
perform create_source_register_attribute(1067,'Код ФИАС',2,4);
perform create_source_register_attribute(1068,'Количество жилых комнат',2,2);
perform create_source_register_attribute(1069,'Количество надземных этажей',2,4);
-- perform create_source_register_attribute(1070,'Количество подземных этажей',2,4);
perform create_source_register_attribute(1071,'Количество подземных этажей родительского здания (сооружения)',2,4);
perform create_source_register_attribute(1072,'Количество этажей родительского здания (сооружения)',2,4);
--perform create_source_register_attribute(1073,'Корпус',2,4);
perform create_source_register_attribute(1074,'Литера (на плане)',2,4);
-- perform create_source_register_attribute(1075,'Материал стен',2,4);
perform create_source_register_attribute(1076,'Материал стен родительского здания (сооружения)',2,4);
perform create_source_register_attribute(1077,'Назначение единого недвижимого комплекса',2,4);
-- perform create_source_register_attribute(1078,'Назначение здания',2,4);
perform create_source_register_attribute(1079,'Назначение здания, если помещение расположено в здании',2,4);
perform create_source_register_attribute(1080,'Назначение по ЕГРОКС',2,4);
-- perform create_source_register_attribute(1081,'Назначение помещения',2,4);
-- perform create_source_register_attribute(1082,'Назначение сооружения',2,4);
perform create_source_register_attribute(1083,'Назначение сооружения, если помещение расположено в сооружении',2,4);
perform create_source_register_attribute(1084,'Назначение текст ОКС (РР)',2,4);
perform create_source_register_attribute(1085,'Наименование',2,4);
perform create_source_register_attribute(1086,'Наименование городского района ',2,4);
--perform create_source_register_attribute(1087,'Наименование документа об утверждении кадастровой стоимости',2,4);
perform create_source_register_attribute(1088,'Наименование объекта культурного наследия РР',2,4);
perform create_source_register_attribute(1089,'Наименование сельсовета ',2,4);
perform create_source_register_attribute(1090,'Наименование участка',2,4);
--perform create_source_register_attribute(1091,'Населенный пункт',2,4);
perform create_source_register_attribute(1092,'Неформализованное описание',2,4);
perform create_source_register_attribute(1093,'Номер (литера) части строения, к которому относится помещение',2,4);
--perform create_source_register_attribute(1094,'Номер акта об утверждении кадастровой стоимости',2,4);
perform create_source_register_attribute(1095,'Номер отчета',2,4);
perform create_source_register_attribute(1096,'Номер помещения',2,4);
-- perform create_source_register_attribute(1097,'Номер этажа',2,4);
perform create_source_register_attribute(1098,'Номер этажа, на котором расположено помещение ',2,4);
perform create_source_register_attribute(1099,'Основная площадь',2,2);
-- perform create_source_register_attribute(1100,'Площадь',2,2);
perform create_source_register_attribute(1101,'Площадь застроенная',2,2);
perform create_source_register_attribute(1102,'Площадь застройки',2,2);
perform create_source_register_attribute(1103,'Погрешность измерения',2,2);
perform create_source_register_attribute(1104,'Подгруппа капитальности',2,4);
--perform create_source_register_attribute(1105,'Положение на ДКК',2,4);
--perform create_source_register_attribute(1106,'Почтовый индекс',2,4);
perform create_source_register_attribute(1107,'Предыдущий кадастровый номер ЗУ',2,4);
perform create_source_register_attribute(1108,'Принадлежности-здания',2,4);
perform create_source_register_attribute(1109,'Принадлежности-сооружения',2,4);
perform create_source_register_attribute(1110,'Проектируемое назначение объекта незавершенного строительства',2,4);
perform create_source_register_attribute(1111,'Протяженность объекта',2,2);
perform create_source_register_attribute(1112,'Процент износа',2,2);
--perform create_source_register_attribute(1113,'Разрешенное использование (текстовое описание)',2,4);
perform create_source_register_attribute(1114,'Район (обл)',2,4);
perform create_source_register_attribute(1115,'Реквизиты документа, на основании которого возникает ограничение (обременение) права',2,4);
perform create_source_register_attribute(1116,'Сведения о включении объекта недвижимости в ЕГРОКН',2,4);
perform create_source_register_attribute(1117,'Сведения о природных объектах',2,4);
perform create_source_register_attribute(1118,'Сведения о частях участка',2,4);
perform create_source_register_attribute(1119,'Сведения об ограничениях (обременениях) прав',2,4);
perform create_source_register_attribute(1120,'Состав единого недвижимого комплекса (кадастровые номера объектов недвижимости)',2,4);
perform create_source_register_attribute(1121,'Способ образования ЗУ  ',2,4);
perform create_source_register_attribute(1122,'Статус ЕГРН',2,4);
perform create_source_register_attribute(1123,'Степень готовности в процентах',2,2);
--perform create_source_register_attribute(1124,'Строение',2,4);
perform create_source_register_attribute(1125,'Строительный объем ',2,2);
perform create_source_register_attribute(1126,'Субъект РФ',2,4);
perform create_source_register_attribute(1127,'Теплоснабжение',2,4);
perform create_source_register_attribute(1128,'Тип города',2,4);
perform create_source_register_attribute(1129,'Тип городского района',2,4);
perform create_source_register_attribute(1130,'Тип населенного пункта',2,4);
perform create_source_register_attribute(1131,'Тип района (обл)',2,4);
perform create_source_register_attribute(1132,'Тип сельсовета ',2,4);
perform create_source_register_attribute(1133,'Тип улицы',2,4);
--perform create_source_register_attribute(1134,'Улица',2,4);
perform create_source_register_attribute(1135,'Условный номер объекта',2,4);
perform create_source_register_attribute(1136,'Уточнение местопол.Расстояние',2,4);
perform create_source_register_attribute(1137,'Электричество',2,4);
perform create_source_register_attribute(1138,'Кадастровый идентификатор',6,4);
perform create_source_register_attribute(1139,'Выброс',7,3);
perform create_source_register_attribute(1140,'Не заполнен',7,3);
perform create_source_register_attribute(1141,'Критерий отнесения в СЗЗ ТБО',8,4);
perform create_source_register_attribute(1142,'X-координата',9,2);
perform create_source_register_attribute(1143,'Y-координата',9,2);
perform create_source_register_attribute(1144,'Доля ОЗИК',9,2);
perform create_source_register_attribute(1145,'Доля СЗЗ',9,2);
perform create_source_register_attribute(1146,'Название ГСК',9,4);
perform create_source_register_attribute(1147,'Наименование ближайшей к населенному пункту дороги федерального значения  ',9,4);
perform create_source_register_attribute(1148,'Наименование ГНП',9,4);
perform create_source_register_attribute(1149,'Наличие вблизи  объекта (-ов), положительно влияющего(-их) на стоимость объектов недвижимости ',9,4);
perform create_source_register_attribute(1150,'Наличие вблизи объекта (-ов) отрицательно влияющего(-их) на стоимость  (свалки и т.п.)',9,4);
perform create_source_register_attribute(1151,'Направление от г Москвы по дорогам федерального значения и основным шоссе (граф)',9,4);
perform create_source_register_attribute(1152,'Направление от г Москвы по сторонам света',9,4);
perform create_source_register_attribute(1153,'Направление от г Москвы по сторонам света (граф)',9,4);
perform create_source_register_attribute(1154,'Обеспеченность территории центральной канализацией',9,4);
perform create_source_register_attribute(1155,'Обеспеченность территории центральным водоснабжением',9,4);
perform create_source_register_attribute(1156,'Обеспеченность территории центральным газоснабжением',9,4);
perform create_source_register_attribute(1157,'Обеспеченность территории центральным теплоснабжением',9,4);
perform create_source_register_attribute(1158,'Обеспеченность территории центральным электроснабжением',9,4);
perform create_source_register_attribute(1159,'Подгруппа привязки',9,2);
perform create_source_register_attribute(1160,'Потенциальное нахождение в элитном посёлке',9,4);
perform create_source_register_attribute(1161,'Расположение объекта относительно ближайшего водного объекта',9,2);
perform create_source_register_attribute(1162,'Расположение объекта относительно ближайшей рекреационной зоны',9,2);
perform create_source_register_attribute(1163,'Расстояние до ближайшего населенного пункта',9,2);
perform create_source_register_attribute(1164,'Расстояние до ближайшей к населенному пункту дороги регионального значения',9,2);
perform create_source_register_attribute(1165,'Расстояние до ближайшей к населенному пункту дороги федерального значения',9,2);
perform create_source_register_attribute(1166,'Расстояние до ближайшей остановки общественного транспорта',9,2);
perform create_source_register_attribute(1167,'Расстояние до ближайшей станции метро',9,2);
perform create_source_register_attribute(1168,'Расстояние до ближайшей станций метро',9,2);
perform create_source_register_attribute(1169,'Расстояние до государственной поликлиники, больницы',9,2);
perform create_source_register_attribute(1170,'Расстояние до государственной поликлиники, больницы (не включая специализированные больницы)',9,2);
perform create_source_register_attribute(1171,'Расстояние до дорог федерального значения и основных шоссе Московской области (граф)',9,2);
perform create_source_register_attribute(1172,'Расстояние до историко-культурного центра населенного пункта',9,2);
perform create_source_register_attribute(1173,'Расстояние до локального центра, отрицательно влияющего на стоимость объектов недвижимости',9,2);
perform create_source_register_attribute(1174,'Расстояние до локального центра, положительно влияющего на стоимость объектов недвижимости',9,2);
perform create_source_register_attribute(1175,'Расстояние до МКАД для расчёта корректировки',9,2);
perform create_source_register_attribute(1176,'Расстояние до общественно-делового центра населенного пункта',9,2);
perform create_source_register_attribute(1177,'Расстояние до остановок общественного транспорта',9,2);
perform create_source_register_attribute(1178,'Расстояние до остановок общественного транспорта (в т.ч. автовокзалы, автостанции и т.п.)',9,2);
perform create_source_register_attribute(1179,'Расстояние до школы или детского сада',9,2);
perform create_source_register_attribute(1180,'Расстояние от населенного пункта до ближайшей железной дороги промышленного назначения',9,2);
perform create_source_register_attribute(1181,'Расстояние от населенного пункта до ближайших ж/д вокзала, станции, платформы',9,2);
perform create_source_register_attribute(1182,'Расстояние от населенного пункта до центра муниципального района (городского округа)',9,2);
perform create_source_register_attribute(1183,'Расстояние от населенного пункта до центра муниципального района, городского округа',9,2);
perform create_source_register_attribute(1184,'Расстояние от объекта до административного центра населенного пункта',9,2);
perform create_source_register_attribute(1185,'Расстояние от объекта до ближайшего водного объекта',9,2);
perform create_source_register_attribute(1186,'Расстояние от объекта до ближайшего Ж\Д вокзала, станции, платформы МО (граф)',9,2);
perform create_source_register_attribute(1187,'Расстояние от объекта до ближайшего Ж\Д вокзала, станции, платформы(граф)',9,2);
perform create_source_register_attribute(1188,'Расстояние от объекта до ближайшей из основных дорог населенного пункта',9,2);
perform create_source_register_attribute(1189,'Расстояние от объекта до ближайшей рекреационной зоны',9,2);
perform create_source_register_attribute(1190,'Расстояние от объекта до МКАД (граф)',9,2);
perform create_source_register_attribute(1191,'Удаленность от объекта до МКАД (кольца)',9,4);
perform create_source_register_attribute(1192,'П.2 Наличие мелиорации',10,3);
perform create_source_register_attribute(1193,'Признак ОЦ С\Х',10,4);
perform create_source_register_attribute(1194,'Диапазон среднемесячной заработной платы в муниципальном образовании',11,2);
perform create_source_register_attribute(1195,'Среднемесячная заработная плата в муниципальном районе (городском округе)',11,2);
perform create_source_register_attribute(1196,'Среднемесячная заработная плата в муниципальном районе, городском округе',11,2);
perform create_source_register_attribute(1197,'Численность населения в населенном пункте',11,2);
perform create_source_register_attribute(1198,'Источник наследования от родителя',12,4);
perform create_source_register_attribute(1199,'Перечень изменений ВОБ',12,4);
perform create_source_register_attribute(1200,'Перечень изменений РР',12,4);
perform create_source_register_attribute(1201,'Признак ВОБ',12,4);
perform create_source_register_attribute(1202,'Без изменения КС (РР)',13,3);
perform create_source_register_attribute(1203,'Внешний износ_ЗП',13,2);
perform create_source_register_attribute(1204,'Высота подвала при наличии, м_ЗП',13,2);
perform create_source_register_attribute(1205,'Высота цоколя, м_ЗП',13,2);
perform create_source_register_attribute(1206,'Высота этажа с перекрытием, м_ЗП',13,2);
perform create_source_register_attribute(1207,'Г_Корректировка на площадь  ',13,2);
perform create_source_register_attribute(1208,'Г_Направление  ',13,4);
perform create_source_register_attribute(1209,'Г_Расстояние до МКАД  ',13,2);
perform create_source_register_attribute(1210,'Г_Сегмент  ',13,4);
perform create_source_register_attribute(1211,'Г_УПКС эталонного объекта  ',13,2);
perform create_source_register_attribute(1212,'Группа расчета',13,4);
perform create_source_register_attribute(1213,'Группа расчета до устранения замечаний',13,4);
perform create_source_register_attribute(1214,'Диапазон площади по справочнику_ЗП',13,4);
perform create_source_register_attribute(1215,'Диапазон строительного объема по справочнику_ЗП',13,4);
perform create_source_register_attribute(1216,'Ед. измерения по справочнику_ЗП',13,4);
perform create_source_register_attribute(1217,'Единица измерения_ЗПС',13,4);
perform create_source_register_attribute(1218,'Кадастровая стоимость',13,2);
perform create_source_register_attribute(1219,'Код по сборнику_ЗП',13,4);
perform create_source_register_attribute(1220,'Кол-во надземных этажей_ЗП',13,2);
perform create_source_register_attribute(1221,'Кол-во подземных этажей_ЗП',13,2);
perform create_source_register_attribute(1222,'Количество единиц измерения_ЗПС',13,4);
perform create_source_register_attribute(1223,'Корректировка машино-места',13,2);
perform create_source_register_attribute(1224,'Корректировка на аварийность',13,2);
perform create_source_register_attribute(1225,'Корректировка на ВРИ',13,2);
perform create_source_register_attribute(1226,'Корректировка на долю ЗУ',13,2);
perform create_source_register_attribute(1227,'Корректировка на ЗОУИТ',13,2);
perform create_source_register_attribute(1228,'Корректировка на КП',13,2);
perform create_source_register_attribute(1229,'Корректировка на масштаб',13,2);
perform create_source_register_attribute(1230,'Корректировка на ТБО',13,2);
perform create_source_register_attribute(1231,'Корректировка подвального помещения',13,2);
perform create_source_register_attribute(1232,'Коэффициент изменения цен_ЗП',13,2);
perform create_source_register_attribute(1233,'Коэффициент сейсмичности_ЗП',13,2);
perform create_source_register_attribute(1234,'КС до устранения замечаний',13,2);
perform create_source_register_attribute(1235,'Метка при расчете методом УПКС',13,4);
perform create_source_register_attribute(1236,'Метод расчета_ЗП',13,4);
perform create_source_register_attribute(1237,'Модельная кадастровая стоимость',13,2);
perform create_source_register_attribute(1238,'Модельный УПКС',13,2);
perform create_source_register_attribute(1239,'Накопленный износ_ЗП',13,2);
perform create_source_register_attribute(1240,'Нормативный срок жизни_ЗП',13,2);
perform create_source_register_attribute(1241,'Площадь по справочнику,  кв. м_ЗП',13,2);
perform create_source_register_attribute(1242,'Поправка к периоду создания_ЗП',13,2);
perform create_source_register_attribute(1243,'Поправка на группу капитальности_ЗП',13,2);
perform create_source_register_attribute(1244,'Поправка на различия в конструктивных характеристиках_ЗП',13,2);
perform create_source_register_attribute(1245,'Поправка на разницу в объеме/площади_ЗП',13,2);
perform create_source_register_attribute(1246,'Прибыль предпринимателя_ЗП',13,2);
perform create_source_register_attribute(1247,'Процент готовности_ЗП',13,2);
perform create_source_register_attribute(1248,'ПС_Вид использования',13,4);
perform create_source_register_attribute(1249,'ПС_Диапазон площади',13,4);
perform create_source_register_attribute(1250,'ПС_Корректировка на площадь',13,2);
perform create_source_register_attribute(1251,'ПС_Линия застройки',13,4);
perform create_source_register_attribute(1252,'ПС_Номер кластера',13,4);
perform create_source_register_attribute(1253,'ПС_Площадь эталонного земельного участка',13,2);
perform create_source_register_attribute(1254,'ПС_Тип автодороги/населенного пункта',13,4);
perform create_source_register_attribute(1255,'ПС_Удаленность от МКАД',13,4);
perform create_source_register_attribute(1256,'ПС_УПКС эталонного участка',13,2);
perform create_source_register_attribute(1257,'Р_Выручка, руб./га  ',13,2);
perform create_source_register_attribute(1258,'Р_ЕСХН, руб./га  ',13,2);
perform create_source_register_attribute(1259,'Р_Затраты на производство, руб./га  ',13,2);
perform create_source_register_attribute(1260,'Р_Земельный налог, руб./га  ',13,2);
perform create_source_register_attribute(1261,'Р_Операционные расходы, руб./га  ',13,2);
perform create_source_register_attribute(1262,'Р_Расстояние до МКАД, км  ',13,2);
perform create_source_register_attribute(1263,'Р_Стоимость земельного участка, руб./га  ',13,2);
perform create_source_register_attribute(1264,'Р_Текущий чистый операционный доход, руб./га  ',13,2);
perform create_source_register_attribute(1265,'Р_Чистый операционный доход без учета налоговых платежей, руб./га  ',13,2);
perform create_source_register_attribute(1266,'Расчетный УПКС',13,4);
perform create_source_register_attribute(1267,'Региональный коэффициент_ЗП',13,2);
perform create_source_register_attribute(1268,'Справочная стоимость 1 ед. измерения, руб._ЗП',13,2);
perform create_source_register_attribute(1269,'Стоимость воспроизводства, руб._ЗП',13,2);
perform create_source_register_attribute(1270,'Строительный объем объекта, куб. м_ЗП',13,2);
perform create_source_register_attribute(1271,'Строительный объем по справочнику,  куб. м_ЗП',13,2);
perform create_source_register_attribute(1272,'Удельный показатель кадастровой стоимости',13,2);
perform create_source_register_attribute(1273,'УПКС до устранения замечаний',13,2);
perform create_source_register_attribute(1274,'Фактор расчета методом УПКС',13,2);
perform create_source_register_attribute(1275,'Физический износ_ЗП',13,2);
perform create_source_register_attribute(1276,'Функциональный износ_ЗП',13,2);
perform create_source_register_attribute(1277,'Год постройки источник',14,4);
perform create_source_register_attribute(1278,'Группа капитальности аналога_ЗП',14,2);
perform create_source_register_attribute(1279,'Группа капитальности_ЗП',14,4);
perform create_source_register_attribute(1280,'Группа расчета_ЗП',14,4);
perform create_source_register_attribute(1281,'Диапазон площади',14,4);
perform create_source_register_attribute(1282,'Дополнительный комментарий (4 группа)',14,4);
perform create_source_register_attribute(1283,'Идентификатор загрузки',14,4);
perform create_source_register_attribute(1284,'Индивидуальный_расчет',14,4);
perform create_source_register_attribute(1285,'Исключение из наследования',14,3);
perform create_source_register_attribute(1286,'Источник года постройки для расчета',14,4);
perform create_source_register_attribute(1287,'Источник информации о виде использования объектов недвижимости',14,4);
perform create_source_register_attribute(1288,'Кадастровый район',14,4);
perform create_source_register_attribute(1289,'КЛАДР_11 знаков',14,4);
perform create_source_register_attribute(1290,'Класс конструктивных элементов_ЗП',14,4);
perform create_source_register_attribute(1291,'КОД ЗДАНИЯ (ДЛЯ ПОМЕЩЕНИЙ)',14,4);
perform create_source_register_attribute(1292,'Код подгруппы здания, сооружения, ОНС',14,4);
perform create_source_register_attribute(1293,'Код расчета вида использования',14,4);
perform create_source_register_attribute(1294,'Комментарий к Коду подгруппы',14,4);
perform create_source_register_attribute(1295,'Комментарий КРВИ',14,4);
perform create_source_register_attribute(1296,'Координаты Яндекс',14,4);
perform create_source_register_attribute(1297,'КС+ГК',14,4);
perform create_source_register_attribute(1298,'Материал стен источник',14,4);
perform create_source_register_attribute(1299,'Машино-место',14,4);
perform create_source_register_attribute(1300,'Муниципальный район или городской округ',14,4);
perform create_source_register_attribute(1301,'Муниципальный район, городской округ',14,4);
perform create_source_register_attribute(1302,'Наличие здания в перечне ОО (для помещений)',14,4);
perform create_source_register_attribute(1303,'Наличие значений ФС',14,4);
perform create_source_register_attribute(1304,'Наличие изменений по ФС',14,4);
perform create_source_register_attribute(1305,'Номер группы ОКС',14,4);
perform create_source_register_attribute(1306,'НОМЕР СЕГМЕНТА ЗДАНИЯ (ДЛЯ ПОМЕЩЕНИЙ)',14,4);
perform create_source_register_attribute(1307,'Номер сегмента ЗУ',14,4);
perform create_source_register_attribute(1308,'Ошибка при анализе перечня',14,4);
perform create_source_register_attribute(1309,'Перечень здания (для помещений)',14,4);
perform create_source_register_attribute(1310,'Перечень уточнений в ходе замечаний к проекту отчета',14,4);
perform create_source_register_attribute(1311,'Подгруппа привязки итог',14,2);
perform create_source_register_attribute(1312,'Полный состав ФС',14,4);
perform create_source_register_attribute(1313,'Признак аварийности',14,4);
perform create_source_register_attribute(1314,'Признак МКД',14,4);
perform create_source_register_attribute(1315,'Признак определение стоимости индексированием РС',14,4);
perform create_source_register_attribute(1316,'Признак передачи ЗП',14,4);
perform create_source_register_attribute(1317,'Признак переноса в Акт',14,4);
perform create_source_register_attribute(1318,'Признак подвального помещения',14,4);
perform create_source_register_attribute(1319,'Признак_торг',14,4);
perform create_source_register_attribute(1320,'Примечание',14,4);
perform create_source_register_attribute(1321,'ПРИМЕЧАНИЕ К КОДУ',14,4);
perform create_source_register_attribute(1322,'Примечание по коду ОКТМО',14,4);
perform create_source_register_attribute(1323,'Прочие_ЗП',14,3);
perform create_source_register_attribute(1324,'Распоряжение МИОМО',14,4);
perform create_source_register_attribute(1325,'Расстояние от объекта до локального центра, отрицательно влияющего на стоимость объектов недвижимост',14,2);
perform create_source_register_attribute(1326,'Расстояние от объекта до локального центра, положительно влияющего на стоимость объектов недвижимост',14,2);
perform create_source_register_attribute(1327,'Статус изменения КС',14,4);
perform create_source_register_attribute(1328,'Строительный объем (уточненный)_ЗП',14,2);
perform create_source_register_attribute(1329,'Сцепка для помещений (Код здания_наименование)',14,4);
perform create_source_register_attribute(1330,'Уточнение на СОД',14,4);
perform create_source_register_attribute(1331,'Уточненный код подгруппы',14,4);
perform create_source_register_attribute(1332,'Уточненный код расчета вида использования',14,4);
perform create_source_register_attribute(1333,'Фактор расчета методом УПКС (пользовательский)',14,2);
perform create_source_register_attribute(1334,'Физический износ (уточненный)_ЗП',14,2);
perform create_source_register_attribute(1335,'Этажность',14,2);
perform create_source_register_attribute(1336,'Этажность источник',14,4);
perform create_source_register_attribute(1337,'Близость к водным объектам',15,4);
perform create_source_register_attribute(1338,'Вид покрытия подъездной дороги',15,4);
perform create_source_register_attribute(1339,'Вид СОД объединения',15,4);
perform create_source_register_attribute(1340,'Возможность подтопления и/или заболоченность',15,4);
perform create_source_register_attribute(1341,'Доступность общественным транспортом',15,4);
perform create_source_register_attribute(1342,'Доступность остановок общественного транспорта (до 30 минут ходьбы)',15,4);
perform create_source_register_attribute(1343,'Название Коттеджного Поселка',15,4);
perform create_source_register_attribute(1344,'Название СОД объединения',15,4);
perform create_source_register_attribute(1345,'Наименование ближайшей к населенному пункту дороги федерального значения',15,4);
perform create_source_register_attribute(1346,'Наименование ближайшей рекреационной зоны',15,4);
perform create_source_register_attribute(1347,'Наименование и тип  водного объекта (река, озеро,   пруд, затопленный карьер и прочее)',15,4);
perform create_source_register_attribute(1348,'Наименование и тип  водного объекта (река, озеро, пруд, затопленный карьер и прочее)',15,4);
perform create_source_register_attribute(1349,'Наименование и тип ближайшего населенного пункта',15,4);
perform create_source_register_attribute(1350,'Наименование объекта (-ов), отрицательно влияющего(-их) на стоимость  (свалки и т.п.)',15,4);
perform create_source_register_attribute(1351,'Наименование объекта (-ов), отрицательно влияющего(-их) на стоимость объектов недвижимости',15,4);
perform create_source_register_attribute(1352,'Наименование объекта (-ов), положительно влияющего(-их) на стоимость объектов недвижимости',15,4);
perform create_source_register_attribute(1353,'Наличие в населенном пункте или вблизи (до 1 км) водного объекта',15,4);
perform create_source_register_attribute(1355,'Наличие в населенном пункте или вблизи (до 1 км) остановок общественного транспорта',15,4);
perform create_source_register_attribute(1356,'Наличие в населенном пункте магазина',15,4);
perform create_source_register_attribute(1357,'Наличие в населенном пункте общеобразовательной школы',15,4);
perform create_source_register_attribute(1358,'Наличие в населенном пункте объектов   здравоохранения',15,4);
perform create_source_register_attribute(1359,'Наличие в населенном пункте объектов здравоохранения',15,4);
perform create_source_register_attribute(1360,'Наличие в населенном пункте центрального водоснабжения',15,4);
perform create_source_register_attribute(1361,'Наличие в населенном пункте центрального газоснабжения',15,4);
perform create_source_register_attribute(1362,'Наличие в населенном пункте центрального теплоснабжения',15,4);
perform create_source_register_attribute(1363,'Наличие в населенном пункте центрального электроснабжения',15,4);
perform create_source_register_attribute(1364,'Наличие в населенном пункте центральной канализации',15,4);
perform create_source_register_attribute(1365,'Наличие в сельском населенном пункте дороги с твердым покрытием',15,4);
perform create_source_register_attribute(1366,'Наличие вблизи  (до 1 км) населенного пункта зон особого режима использования (кладбищ и т.п.)',15,4);
perform create_source_register_attribute(1367,'Наличие вблизи (до 1 км) населенного пункта зон особого режима использования (кладбищ и т.д.)',15,4);
perform create_source_register_attribute(1368,'Наличие вблизи (до 1 км) населенного пункта зон разработки полезных ископаемых',15,4);
perform create_source_register_attribute(1369,'Наличие вблизи (до 1 км) населенного пункта рекреационной зоны (лесной массив, парковая зона и т.п.)',15,4);
perform create_source_register_attribute(1370,'Наличие вблизи объекта (-ов) отрицательно влияющего(-их) на стоимость (свалки и т.п.)',15,4);
perform create_source_register_attribute(1371,'Наличие вблизи объекта (-ов), положительно влияющего(-их) на стоимость объектов недвижимости',15,4);
perform create_source_register_attribute(1372,'Наличие свободного доступа к водному объекту на расстоянии до 1 км',15,4);
perform create_source_register_attribute(1373,'Наличие свободного доступа к лесу на расстоянии до 1 км',15,4);
perform create_source_register_attribute(1374,'Наличие центрального водоснабжения (более чем у 50% участков)',15,4);
perform create_source_register_attribute(1375,'Наличие центрального газоснабжения (более чем у 50% участков)',15,4);
perform create_source_register_attribute(1376,'Наличие центрального электроснабжения (более чем у 50% участков)',15,4);
perform create_source_register_attribute(1377,'Направление от г Москвы по основным шоссе (сем)',15,4);
perform create_source_register_attribute(1378,'Направление от г Москвы по сторонам света (сем)',15,4);
perform create_source_register_attribute(1379,'Расстояние от населенного пункта до ближайший железной дороги промышленного назначения ',15,2);
perform create_source_register_attribute(1380,'Расстояние от населенного пункта до МКАД',15,2);
perform create_source_register_attribute(1381,'Расстояние от населенного пункта до МКАД (сем)',15,2);
perform create_source_register_attribute(1382,'Расстояние от объекта до одного из центров населенного пункта',15,2);
perform create_source_register_attribute(1383,'Расстояние от СОД объединения или КП до МКАД (сем)',15,2);
perform create_source_register_attribute(1384,'Расстояние от СОД объединения или КП до центра муниципального района (городского округа)',15,2);
perform create_source_register_attribute(1385,'Статус населенного пункта',15,4);
perform create_source_register_attribute(1386,'Число рейсов общественного транспорта (всех видов)',15,4);
perform create_source_register_attribute(1387,'Источник кода на 01.01.2018',16,4);
perform create_source_register_attribute(1388,'КОД на 01.01.2018',16,4);
perform create_source_register_attribute(1389,'КС на 01.01.2018',16,2);
perform create_source_register_attribute(1390,'УПКС на 01.01.2018',16,2);
perform create_source_register_attribute(1391,'КОД по справочнику',17,4);
perform create_source_register_attribute(1392,'Год постройки для расчета',18,2);
perform create_source_register_attribute(1393,'Действительный возраст_ЗП',18,4);
perform create_source_register_attribute(1394,'Материал стен (основной)',18,4);
perform create_source_register_attribute(1395,'Наличие коммуникаций',18,4);
perform create_source_register_attribute(1396,'Обеспеченность территории центральными коммуникациями',18,4);
perform create_source_register_attribute(1397,'П.1 Превышение кадастровой стоимости более 10%',18,4);
perform create_source_register_attribute(1398,'Площадь для модели',18,2);
perform create_source_register_attribute(1399,'Расстояние до МКАД (кольцами по 10 км)',18,2);
perform create_source_register_attribute(1400,'Территориальная зона',18,4);
perform create_source_register_attribute(1401,'Удаленность по кольцам_Направление по шоссе',18,4);
perform create_source_register_attribute(1402,'Удаленность_кольца+Направление по сторонам света',18,4);
perform create_source_register_attribute(1403,'Зона ЗУ',19,4);
perform create_source_register_attribute(1404,'Дата источника информации',20,5);
perform create_source_register_attribute(1405,'Идентификатор объекта-аналога ЦИАН',20,4);
perform create_source_register_attribute(1406,'Источник информации',20,4);
perform create_source_register_attribute(1407,'Номер источника информации',20,4);
perform create_source_register_attribute(1408,'Описание объекта-аналога',20,4);
perform create_source_register_attribute(1409,'Полная цена',20,2);
perform create_source_register_attribute(1410,'Текст объявления',20,4);
perform create_source_register_attribute(1411,'Тип цены',20,4);
perform create_source_register_attribute(1412,'Удельная цена',20,2);
perform create_source_register_attribute(1413,'Код расчета вида использования для ЗУ',21,4);
perform create_source_register_attribute(1414,'Код по справочнику МЭР',22,4);
perform create_source_register_attribute(1415,'Источник предшественника',23,4);

perform create_source_register_tables_from_records(3);
perform create_source_register_tables_from_records(4);
perform create_source_register_tables_from_records(5);
perform create_source_register_tables_from_records(2);
perform create_source_register_tables_from_records(6);
perform create_source_register_tables_from_records(7);
perform create_source_register_tables_from_records(8);
perform create_source_register_tables_from_records(9);
perform create_source_register_tables_from_records(10);
perform create_source_register_tables_from_records(11);
perform create_source_register_tables_from_records(12);
perform create_source_register_tables_from_records(13);
perform create_source_register_tables_from_records(14);
perform create_source_register_tables_from_records(15);
perform create_source_register_tables_from_records(16);
perform create_source_register_tables_from_records(17);
perform create_source_register_tables_from_records(18);
perform create_source_register_tables_from_records(19);
perform create_source_register_tables_from_records(20);
perform create_source_register_tables_from_records(21);
perform create_source_register_tables_from_records(22);
perform create_source_register_tables_from_records(23);
END $$;