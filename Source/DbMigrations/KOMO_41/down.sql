﻿--Добавляем виджет для процедуры проверки на выбросы из рабочего стола Объектов-Ананлогов
INSERT INTO dashboards_panel (id, dashboard_id, title, column_index, order_in_column, panel_type_id, settings) 
VALUES (1000051, 1000002, 'Страница в системе', 0, 9, 5, '<PanelPartialViewDto><Url>~/Views/MarketObjects/OutliersChecking.cshtml</Url><Id>-1</Id><Title></Title><WindowWidth></WindowWidth><WindowHeight></WindowHeight><SrdFunctionTag></SrdFunctionTag></PanelPartialViewDto>');