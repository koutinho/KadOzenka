--Удаляем виджет для процедуры проверки на выбросы из рабочего стола Объектов-Ананлогов
delete from dashboards_panel where id=1000051;

--Удаляем виджет для процедуры проверки данных на дублирование
delete from dashboards_panel where id=1000008;

--Удаляем таблицы для корректировок
delete from core_register_attribute where registerid in (108, 111, 112, 113, 114, 115, 116, 117);
delete from core_register where registerid in (108, 111, 112, 113, 114, 115, 116, 117);
drop table MARKET_INDEXES_FOR_DATE_CORRECTION;
drop table MARKET_COEFFICIENT_FOR_ROOMS_CORRECTION;
drop table MARKET_PRICE_CORRECTION_BY_STAGE_HISTORY;
drop table MARKET_PRICE_AFTER_CORRECTION_BY_ROOMS_H;
drop table MARKET_COEFFICIENTS_FOR_FIRST_FLOOR_CORR;
drop table MARKET_PRICE_FOR_FIRST_FLOOR_HISTORY;
drop table MARKET_PRICE_AFTER_CORRECTION_BY_DATE_H;
drop table MARKET_CORRECTION_SETTINGS;


--Удаляем процессы
delete from core_long_process_type where id in (24, 25, 26, 27);