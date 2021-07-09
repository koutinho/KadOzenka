--удаляем атрибут реестра с аналогами, который, судя по всему, был добавлен случайно
delete from core_register_attribute where id=10008300;

--удаляем связь между Аналогом и ОН (не использовалась в коде, мешала разделению проектов)
delete from core_register_relation where id=103;

--удаляем связь между ЕО и справочником кадастровых кварталов (не использовалась в коде, мешала разделению проектов)
delete from core_register_relation where id=218;

--Удаляем таблицы, связанные с экспресс-оценкой
delete from core_register_attribute where registerid in (608, 609, 610, 611);
delete from core_register where registerid in (608, 609, 610, 611);
DROP TABLE IF EXISTS ES_TO_MARKET_CORE_OBJECT;
DROP TABLE IF EXISTS ES_REFERENCE;
DROP TABLE IF EXISTS ES_REFERENCE_ITEM;
DROP TABLE IF EXISTS ES_SETTINGS_PARAMS;

--Удаляем дашборд экспресс-оценки из стартовой страницы
delete from dashboards_panel where id=1000041;