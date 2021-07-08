--наполнение данными
DO $$
BEGIN
--CadastralCost
perform create_source_register_attribute(1416,'Кадастровый номер',2,4);
perform create_source_register_attribute(1417,'Дата определения кадастровой стоимости',2,5);
perform create_source_register_attribute(1418,'Дата внесения сведений о кадастровой стоимости в ЕГРН',2,5);
perform create_source_register_attribute(1419,'Дата утверждения кадастровой стоимости',2,5);
perform create_source_register_attribute(1420,'Номер акта об утверждении кадастровой стоимости',2,4);
perform create_source_register_attribute(1421,'Дата акта об утверждении кадастровой стоимости',2,5);
perform create_source_register_attribute(1422,'Дата начала применения кадастровой стоимости',2,5);
perform create_source_register_attribute(1423,'Дата подачи заявления о пересмотре кадастровой стоимости',2,5);
perform create_source_register_attribute(1424,'В соответствии с Федеральным законом от 3 июля 2016 г. N 360-ФЗ "О внесении изменений в отдельные законодательные акты Российской Федерации" применяется с',2,5);
perform create_source_register_attribute(1425,'Наименование документа об утверждении кадастровой стоимости',2,4);

-- Address
perform create_source_register_attribute(1426,'Уникальный номер адресообразующего элемента в государственном адресном реестре',2,4);
perform create_source_register_attribute(1427,'Код ОКАТО',2,4);
perform create_source_register_attribute(1428,'Код КЛАДР',2,4);
perform create_source_register_attribute(1429,'Код OKTMO',2,4);
perform create_source_register_attribute(1430,'Почтовый индекс',2,4);
perform create_source_register_attribute(1431,'Российская Федерация',2,4);
perform create_source_register_attribute(1432,'Код региона',2,4);
perform create_source_register_attribute(1433,'Район',2,4);
perform create_source_register_attribute(1434,'Муниципальное образование',2,4);
perform create_source_register_attribute(1435,'Городской район',2,4);
perform create_source_register_attribute(1436,'Сельсовет',2,4);
perform create_source_register_attribute(1437,'Населенный пункт',2,4);
perform create_source_register_attribute(1438,'Элемент планировочной структуры',2,4);
perform create_source_register_attribute(1439,'Улица',2,4);
perform create_source_register_attribute(1440,'Дом',2,4);
perform create_source_register_attribute(1441,'Корпус',2,4);
perform create_source_register_attribute(1442,'Строение',2,4);
perform create_source_register_attribute(1443,'Квартира',2,4);
perform create_source_register_attribute(1444,'Иное',2,4);
perform create_source_register_attribute(1445,'Адрес или местоположение',2,4);
perform create_source_register_attribute(1446,'Расположение ориентира в границах участка',2,4);
perform create_source_register_attribute(1447,'Положение на ДКК',2,4);
perform create_source_register_attribute(1448,'Уточнение местоположения: Наименование ориентира',2,4);
perform create_source_register_attribute(1449,'Уточнение местоположения: Расстояние',2,4);
perform create_source_register_attribute(1450,'Уточнение местоположения: Направление',2,4);

perform create_source_register_attribute(1451,'Вид (виды) разрешенного использования',2,4);

-- Sub building
perform create_source_register_attribute(1452,'Сведения о части здания или помещения [1]: Площадь в квадратных метрах',2,2);
perform create_source_register_attribute(1453,'Сведения о части здания или помещения [1]: Сведения об ограничениях (обременениях) [1]: Содержание ограничения',2,4);
perform create_source_register_attribute(1454,'Сведения о части здания или помещения [1]: Сведения об ограничениях (обременениях) [1]: Код по справочнику',2,4);
perform create_source_register_attribute(1455,'Сведения о части здания или помещения [1]: Сведения об ограничениях (обременениях) [1]: Номер государственной регистрации',2,4);
perform create_source_register_attribute(1456,'Сведения о части здания или помещения [1]: Сведения об ограничениях (обременениях) [1]: Дата государственной регистрации',2,5);

perform create_source_register_attribute(1457,'Сведения о части здания или помещения [1]: Код документа',2,4);
perform create_source_register_attribute(1458,'Сведения о части здания или помещения [1]: Наименование документа',2,4);
perform create_source_register_attribute(1459,'Сведения о части здания или помещения [1]: Серия документа',2,4);
perform create_source_register_attribute(1460,'Сведения о части здания или помещения [1]: Номер документа',2,4);
perform create_source_register_attribute(1461,'Сведения о части здания или помещения [1]: Дата документа',2,5);
perform create_source_register_attribute(1462,'Сведения о части здания или помещения [1]: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1463,'Сведения о части здания или помещения [1]: Особые отметки',2,4);

perform create_source_register_attribute(1464,'Сведения о части здания или помещения [1]: Учетный номер части',2,4);
perform create_source_register_attribute(1465,'Сведения о части здания или помещения [1]: Дата внесения в ЕГРН сведений о части',2,5);

perform create_source_register_attribute(1466,'Кадастровые номера помещений, расположенных в объекте недвижимости',2,4);
perform create_source_register_attribute(1467,'Кадастровые номера машино-мест, расположенных в объекте недвижимости',2,4);
perform create_source_register_attribute(1468,'Кадастровый номер единого недвижимого комплекса, если объект недвижимости входит в состав единого недвижимого комплекса',2,4);
perform create_source_register_attribute(1469,'Кадастровый номер предприятия как имущественного комплекса, если объект недвижимости входит в состав предприятия как имущественного комплекса',2,4);
perform create_source_register_attribute(1470,'Назначение предприятия как имущественного комплекса, если объект недвижимости входит в состав предприятия как имущественного комплекса',2,4);

-- CulturalHeritage
perform create_source_register_attribute(1471,'Включение объекта недвижимости в ЕГРОКН: Регистрационный номер',2,4);
perform create_source_register_attribute(1472,'Включение объекта недвижимости в ЕГРОКН: Вид объекта',2,4);
perform create_source_register_attribute(1473,'Включение объекта недвижимости в ЕГРОКН: Наименование',2,4);
perform create_source_register_attribute(1474,'Требования к сохранению, содержанию и использованию, обеспечению доступа',2,4);
perform create_source_register_attribute(1475,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Код документа',2,4);
perform create_source_register_attribute(1476,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Наименование документа',2,4);
perform create_source_register_attribute(1477,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Серия документа',2,4);
perform create_source_register_attribute(1478,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Номер документа',2,4);
perform create_source_register_attribute(1479,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Дата документа',2,5);
perform create_source_register_attribute(1480,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1481,'Реквизиты соответствующего решения о включении объекта в ЕГРОКН: Особые отметки',2,4);

--KeyParameters
perform create_source_register_attribute(1482,'Основные характеристики [1]: Тип характеристики',2,4);
perform create_source_register_attribute(1483,'Основные характеристики [1]: Значение (величина в метрах (кв. метрах для площади, куб. метрах для объема))',2,2);

-- Sub Construction
perform create_source_register_attribute(1484,'Сведения о части сооружения [1]: Основная характеристика,тип характеристики',2,4);
perform create_source_register_attribute(1485,'Сведения о части сооружения [1]: Основная характеристика, значение (величина в метрах (кв. метрах для площади, куб. метрах для объема))',2,2);

perform create_source_register_attribute(1486,'Сведения о части сооружения [1]: Сведения об ограничениях (обременениях) [1]: Содержание ограничения',2,4);
perform create_source_register_attribute(1487,'Сведения о части сооружения [1]: Сведения об ограничениях (обременениях) [1]: Код по справочнику',2,4);
perform create_source_register_attribute(1488,'Сведения о части сооружения [1]: Сведения об ограничениях (обременениях) [1]: Номер государственной регистрации',2,4);
perform create_source_register_attribute(1489,'Сведения о части сооружения [1]: Сведения об ограничениях (обременениях) [1]: Дата государственной регистрации',2,5);

perform create_source_register_attribute(1490,'Сведения о части сооружения [1]: Код документа',2,4);
perform create_source_register_attribute(1491,'Сведения о части сооружения [1]: Наименование документа',2,4);
perform create_source_register_attribute(1492,'Сведения о части сооружения [1]: Серия документа',2,4);
perform create_source_register_attribute(1493,'Сведения о части сооружения [1]: Номер документа',2,4);
perform create_source_register_attribute(1494,'Сведения о части сооружения [1]: Дата документа',2,5);
perform create_source_register_attribute(1495,'Сведения о части сооружения [1]: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1496,'Сведения о части сооружения [1]: Особые отметки',2,4);

perform create_source_register_attribute(1497,'Сведения о части сооружения [1]: Учетный номер части',2,4);
perform create_source_register_attribute(1498,'Сведения о части сооружения [1]: Дата внесения в ЕГРН сведений о части',2,5);

perform create_source_register_attribute(1499,'Вид объекта недвижимости, в котором расположено помещение, машино-место',2,4);

perform create_source_register_attribute(1500,'Вид жилого помещения специализированного жилищного фонда',2,4);
perform create_source_register_attribute(1501,'Нежилое помещение - общее имущество в многоквартирном доме',2,3);
perform create_source_register_attribute(1502,'Нежилое помещение - помещение вспомогательного использования',2,3);

perform create_source_register_attribute(1504,'Номер на плане',2,4);
perform create_source_register_attribute(1505,'Описание расположения',2,4);

perform create_source_register_attribute(1506,'Кадастровые номера объектов недвижимости, расположенных в пределах земельного участка',2,4);
perform create_source_register_attribute(1507,'Погрешность измерения площади',2,2);
perform create_source_register_attribute(1508,'Вид разрешенного использования земельного участка в соответствии с классификатором, утвержденным приказом Минэкономразвития России от 01.09.2014 № 540',2,4);
perform create_source_register_attribute(1509,'Разрешенное использование (текстовое описание)',2,4);

--NaturalObjects 
perform create_source_register_attribute(1510,'Природный объект на участке [1]: Вид объекта',2,4);
perform create_source_register_attribute(1511,'Природный объект на участке [1]: Наименование лесничества (лесопарка), участкового лесничества',2,4);
perform create_source_register_attribute(1512,'Природный объект на участке [1]: Целевое назначение (категория) лесов',2,4);
perform create_source_register_attribute(1513,'Природный объект на участке [1]: Номера лесных кварталов',2,4);
perform create_source_register_attribute(1514,'Природный объект на участке [1]: Номера лесотаксационных выделов',2,4);
perform create_source_register_attribute(1515,'Природный объект на участке [1]: Категория защитных лесов',2,4);
perform create_source_register_attribute(1516,'Природный объект на участке [1]: Вид разрешенного использования лесов [1]',2,4);
perform create_source_register_attribute(1517,'Вид водного объекта',2,4);
perform create_source_register_attribute(1518,'Наименование водного объекта, иного природного объекта',2,4);
perform create_source_register_attribute(1519,'Характеристика иного природного объекта',2,4);

--Sub Parcels 
perform create_source_register_attribute(1520,'Сведения о части участка [1]: Площадь',2,2);
perform create_source_register_attribute(1521,'Сведения о части участка [1]: Погрешность измерения площади',2,2);
perform create_source_register_attribute(1522,'Сведения о части участка [1]: Сведения об ограничениях (обременениях) [1]: Содержание ограничения',2,4);
perform create_source_register_attribute(1523,'Сведения о части участка [1]: Сведения об ограничениях (обременениях) [1]: Код по справочнику',2,4);
perform create_source_register_attribute(1524,'Сведения о части участка [1]: Сведения об ограничениях (обременениях) [1]: Реестровый номер границы зоны, территории',2,4);
perform create_source_register_attribute(1525,'Сведения о части участка [1]: Сведения об ограничениях (обременениях) [1]: Кадастровый номер ЗУ, в пользу которого установлен сервитут',2,4);
perform create_source_register_attribute(1526,'Сведения о части участка [1]: Сведения об ограничениях (обременениях) [1]: Номер государственной регистрации',2,4);
perform create_source_register_attribute(1527,'Сведения о части участка [1]: Сведения об ограничениях (обременениях) [1]: Дата государственной регистрации',2,5);

perform create_source_register_attribute(1528,'Сведения о части участка [1]: Код документа',2,4);
perform create_source_register_attribute(1529,'Сведения о части участка [1]: Наименование документа',2,4);
perform create_source_register_attribute(1530,'Сведения о части участка [1]: Серия документа',2,4);
perform create_source_register_attribute(1531,'Сведения о части участка [1]: Номер документа',2,4);
perform create_source_register_attribute(1532,'Сведения о части участка [1]: Дата документа',2,5);
perform create_source_register_attribute(1533,'Сведения о части участка [1]: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1534,'Сведения о части участка [1]: Особые отметки',2,4);

perform create_source_register_attribute(1535,'Сведения о части участка [1]: Учетный номер части',2,4);
perform create_source_register_attribute(1536,'Сведения о части участка [1]: Дата внесения в ЕГРН сведений о части',2,5);

-- ZonesAndTerritories
perform create_source_register_attribute(1537,'Сведения о расположении земельного участка в границах зоны или территории [1]: Наименование',2,4);
perform create_source_register_attribute(1538,'Сведения о расположении земельного участка в границах зоны или территории [1]: Вид или наименование по документу',2,4);
perform create_source_register_attribute(1539,'Сведения о расположении земельного участка в границах зоны или территории [1]: Реестровый номер границы',2,4);
perform create_source_register_attribute(1540,'Сведения о расположении земельного участка в границах зоны или территории [1]: Содержание ограничения',2,4);
perform create_source_register_attribute(1541,'Сведения о расположении земельного участка в границах зоны или территории [1]: Полностью входит в зону',2,3);

perform create_source_register_attribute(1542,'Сведения о расположении земельного участка в границах зоны или территории [1]: Код документа',2,4);
perform create_source_register_attribute(1543,'Сведения о расположении земельного участка в границах зоны или территории [1]: Наименование документа',2,4);
perform create_source_register_attribute(1544,'Сведения о расположении земельного участка в границах зоны или территории [1]: Серия документа',2,4);
perform create_source_register_attribute(1545,'Сведения о расположении земельного участка в границах зоны или территории [1]: Номер документа',2,4);
perform create_source_register_attribute(1546,'Сведения о расположении земельного участка в границах зоны или территории [1]: Дата документа',2,5);
perform create_source_register_attribute(1547,'Сведения о расположении земельного участка в границах зоны или территории [1]: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1548,'Сведения о расположении земельного участка в границах зоны или территории [1]: Особые отметки',2,4);

-- GovernmentLandSupervision
perform create_source_register_attribute(1549,'Сведения о результатах проведения государственного земельного надзора [1]: Наименование органа',2,4);
perform create_source_register_attribute(1550,'Сведения о результатах проведения государственного земельного надзора [1]: Мероприятие',2,4);
perform create_source_register_attribute(1551,'Сведения о результатах проведения государственного земельного надзора [1]: Форма проведения',2,4);
perform create_source_register_attribute(1552,'Сведения о результатах проведения государственного земельного надзора [1]: Дата окончания проверки',2,5);
perform create_source_register_attribute(1553,'Сведения о результатах проведения государственного земельного надзора [1]: Наличие нарушения',2,3);
perform create_source_register_attribute(1554,'Сведения о результатах проведения государственного земельного надзора [1]: Вид выявленного правонарушения',2,4);
perform create_source_register_attribute(1555,'Сведения о результатах проведения государственного земельного надзора [1]: Признаки выявленного правонарушения',2,4);
perform create_source_register_attribute(1556,'Сведения о результатах проведения государственного земельного надзора [1]: Площадь (в кв. м)',2,2);

perform create_source_register_attribute(1557,'Сведения о результатах проведения государственного земельного надзора [1]: Код оформленного документа',2,4);
perform create_source_register_attribute(1558,'Сведения о результатах проведения государственного земельного надзора [1]: Наименование оформленного документа',2,4);
perform create_source_register_attribute(1559,'Сведения о результатах проведения государственного земельного надзора [1]: Серия оформленного документа',2,4);
perform create_source_register_attribute(1560,'Сведения о результатах проведения государственного земельного надзора [1]: Номер оформленного документа',2,4);
perform create_source_register_attribute(1561,'Сведения о результатах проведения государственного земельного надзора [1]: Дата оформленного документа',2,5);
perform create_source_register_attribute(1562,'Сведения о результатах проведения государственного земельного надзора [1]: Организация, выдавшая оформленный документ. Автор документа',2,4);
perform create_source_register_attribute(1563,'Сведения о результатах проведения государственного земельного надзора [1]: Особые отметки оформленного документа',2,4);

perform create_source_register_attribute(1564,'Сведения о результатах проведения государственного земельного надзора [1]: Отметка об устранении выявленного нарушения',2,3);
perform create_source_register_attribute(1565,'Сведения о результатах проведения государственного земельного надзора [1]: Наименование органа, принявшего решение об устранении правонарушения',2,4);

perform create_source_register_attribute(1566,'Сведения о результатах проведения государственного земельного надзора [1]: Код документа об устранении правонарушения',2,4);
perform create_source_register_attribute(1567,'Сведения о результатах проведения государственного земельного надзора [1]: Наименованиедокумента об устранении правонарушения',2,4);
perform create_source_register_attribute(1568,'Сведения о результатах проведения государственного земельного надзора [1]: Серия документа об устранении правонарушения',2,4);
perform create_source_register_attribute(1569,'Сведения о результатах проведения государственного земельного надзора [1]: Номер документа об устранении правонарушения',2,4);
perform create_source_register_attribute(1570,'Сведения о результатах проведения государственного земельного надзора [1]: Дата документа об устранении правонарушения',2,5);
perform create_source_register_attribute(1571,'Сведения о результатах проведения государственного земельного надзора [1]: Организация, выдавшая документ об устранении правонарушения. Автор документа',2,4);
perform create_source_register_attribute(1572,'Сведения о результатах проведения государственного земельного надзора [1]: Особые отметки документа об устранении правонарушения',2,4);

--SurveyingProject
perform create_source_register_attribute(1573,'Сведения проекте межевания территории: Учетный номер утвержденного проекта межевания территории',2,4);
perform create_source_register_attribute(1574,'Сведения проекте межевания территории: Код документа',2,4);
perform create_source_register_attribute(1575,'Сведения проекте межевания территории: Наименование документа',2,4);
perform create_source_register_attribute(1576,'Сведения проекте межевания территории: Серия документа',2,4);
perform create_source_register_attribute(1577,'Сведения проекте межевания территории: Номер документа',2,4);
perform create_source_register_attribute(1578,'Сведения проекте межевания территории: Дата документа',2,5);
perform create_source_register_attribute(1579,'Сведения проекте межевания территории: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1580,'Сведения проекте межевания территории: Особые отметки',2,4);

--HiredHouse
perform create_source_register_attribute(1581,'Вид использования наёмного дома',2,4);
perform create_source_register_attribute(1582,'Акт о предоставлении участка для строительства наемного дома',2,3);
perform create_source_register_attribute(1583,'Акт о предоставлении участка для освоения территории в целях строительства и эксплуатации наемного дома',2,3);
perform create_source_register_attribute(1584,'Договор о предоставлении участка для строительства наемного дома',2,4);
perform create_source_register_attribute(1585,'Договор о предоставлении участка для освоения территории в целях строительства и эксплуатации наемного дома',2,4);
perform create_source_register_attribute(1586,'Решение собственника земельного участка, находящегося в частной собственности, о строительстве наемного дома',2,3);
perform create_source_register_attribute(1587,'Договор о предоставлении поддержки для создания и эксплуатации наемного дома социального использования',2,4);
perform create_source_register_attribute(1588,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Код документа',2,4);
perform create_source_register_attribute(1589,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Наименование документа',2,4);
perform create_source_register_attribute(1590,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Серия документа',2,4);
perform create_source_register_attribute(1591,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Номер документа',2,4);
perform create_source_register_attribute(1592,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Дата документа',2,5);
perform create_source_register_attribute(1593,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Организация, выдавшая документ. Автор документа',2,4);
perform create_source_register_attribute(1594,'Сведения о создании (эксплуатации) на земельном участке наёмного дома: Особые отметки',2,4);

perform create_source_register_attribute(1595,'Сведения об ограничении оборотоспособности земельного участка в соответствии со статьей 11 Федерального закона от 1 мая 2016 г. № 119-ФЗ',2,4);


perform create_source_register_table_for_attribute_partitioning(1416);
perform create_source_register_table_for_attribute_partitioning(1417);
perform create_source_register_table_for_attribute_partitioning(1418);
perform create_source_register_table_for_attribute_partitioning(1419);
perform create_source_register_table_for_attribute_partitioning(1420);
perform create_source_register_table_for_attribute_partitioning(1421);
perform create_source_register_table_for_attribute_partitioning(1422);
perform create_source_register_table_for_attribute_partitioning(1423);
perform create_source_register_table_for_attribute_partitioning(1424);
perform create_source_register_table_for_attribute_partitioning(1425);
perform create_source_register_table_for_attribute_partitioning(1426);
perform create_source_register_table_for_attribute_partitioning(1427);
perform create_source_register_table_for_attribute_partitioning(1428);
perform create_source_register_table_for_attribute_partitioning(1429);
perform create_source_register_table_for_attribute_partitioning(1430);
perform create_source_register_table_for_attribute_partitioning(1431);
perform create_source_register_table_for_attribute_partitioning(1432);
perform create_source_register_table_for_attribute_partitioning(1433);
perform create_source_register_table_for_attribute_partitioning(1434);
perform create_source_register_table_for_attribute_partitioning(1435);
perform create_source_register_table_for_attribute_partitioning(1436);
perform create_source_register_table_for_attribute_partitioning(1437);
perform create_source_register_table_for_attribute_partitioning(1438);
perform create_source_register_table_for_attribute_partitioning(1439);
perform create_source_register_table_for_attribute_partitioning(1440);
perform create_source_register_table_for_attribute_partitioning(1441);
perform create_source_register_table_for_attribute_partitioning(1442);
perform create_source_register_table_for_attribute_partitioning(1443);
perform create_source_register_table_for_attribute_partitioning(1444);
perform create_source_register_table_for_attribute_partitioning(1445);
perform create_source_register_table_for_attribute_partitioning(1446);
perform create_source_register_table_for_attribute_partitioning(1447);
perform create_source_register_table_for_attribute_partitioning(1448);
perform create_source_register_table_for_attribute_partitioning(1449);
perform create_source_register_table_for_attribute_partitioning(1450);
perform create_source_register_table_for_attribute_partitioning(1451);
perform create_source_register_table_for_attribute_partitioning(1452);
perform create_source_register_table_for_attribute_partitioning(1453);
perform create_source_register_table_for_attribute_partitioning(1454);
perform create_source_register_table_for_attribute_partitioning(1455);
perform create_source_register_table_for_attribute_partitioning(1456);
perform create_source_register_table_for_attribute_partitioning(1457);
perform create_source_register_table_for_attribute_partitioning(1458);
perform create_source_register_table_for_attribute_partitioning(1459);
perform create_source_register_table_for_attribute_partitioning(1460);
perform create_source_register_table_for_attribute_partitioning(1461);
perform create_source_register_table_for_attribute_partitioning(1462);
perform create_source_register_table_for_attribute_partitioning(1463);
perform create_source_register_table_for_attribute_partitioning(1464);
perform create_source_register_table_for_attribute_partitioning(1465);
perform create_source_register_table_for_attribute_partitioning(1466);
perform create_source_register_table_for_attribute_partitioning(1467);
perform create_source_register_table_for_attribute_partitioning(1468);
perform create_source_register_table_for_attribute_partitioning(1469);
perform create_source_register_table_for_attribute_partitioning(1470);
perform create_source_register_table_for_attribute_partitioning(1471);
perform create_source_register_table_for_attribute_partitioning(1472);
perform create_source_register_table_for_attribute_partitioning(1473);
perform create_source_register_table_for_attribute_partitioning(1474);
perform create_source_register_table_for_attribute_partitioning(1475);
perform create_source_register_table_for_attribute_partitioning(1476);
perform create_source_register_table_for_attribute_partitioning(1477);
perform create_source_register_table_for_attribute_partitioning(1478);
perform create_source_register_table_for_attribute_partitioning(1479);
perform create_source_register_table_for_attribute_partitioning(1480);
perform create_source_register_table_for_attribute_partitioning(1481);
perform create_source_register_table_for_attribute_partitioning(1482);
perform create_source_register_table_for_attribute_partitioning(1483);
perform create_source_register_table_for_attribute_partitioning(1484);
perform create_source_register_table_for_attribute_partitioning(1485);
perform create_source_register_table_for_attribute_partitioning(1486);
perform create_source_register_table_for_attribute_partitioning(1487);
perform create_source_register_table_for_attribute_partitioning(1488);
perform create_source_register_table_for_attribute_partitioning(1489);
perform create_source_register_table_for_attribute_partitioning(1490);
perform create_source_register_table_for_attribute_partitioning(1491);
perform create_source_register_table_for_attribute_partitioning(1492);
perform create_source_register_table_for_attribute_partitioning(1493);
perform create_source_register_table_for_attribute_partitioning(1494);
perform create_source_register_table_for_attribute_partitioning(1495);
perform create_source_register_table_for_attribute_partitioning(1496);
perform create_source_register_table_for_attribute_partitioning(1497);
perform create_source_register_table_for_attribute_partitioning(1498);
perform create_source_register_table_for_attribute_partitioning(1499);
perform create_source_register_table_for_attribute_partitioning(1500);
perform create_source_register_table_for_attribute_partitioning(1501);
perform create_source_register_table_for_attribute_partitioning(1502);
perform create_source_register_table_for_attribute_partitioning(1504);
perform create_source_register_table_for_attribute_partitioning(1505);
perform create_source_register_table_for_attribute_partitioning(1506);
perform create_source_register_table_for_attribute_partitioning(1507);
perform create_source_register_table_for_attribute_partitioning(1508);
perform create_source_register_table_for_attribute_partitioning(1509);
perform create_source_register_table_for_attribute_partitioning(1510);
perform create_source_register_table_for_attribute_partitioning(1511);
perform create_source_register_table_for_attribute_partitioning(1512);
perform create_source_register_table_for_attribute_partitioning(1513);
perform create_source_register_table_for_attribute_partitioning(1514);
perform create_source_register_table_for_attribute_partitioning(1515);
perform create_source_register_table_for_attribute_partitioning(1516);
perform create_source_register_table_for_attribute_partitioning(1517);
perform create_source_register_table_for_attribute_partitioning(1518);
perform create_source_register_table_for_attribute_partitioning(1519);
perform create_source_register_table_for_attribute_partitioning(1520);
perform create_source_register_table_for_attribute_partitioning(1521);
perform create_source_register_table_for_attribute_partitioning(1522);
perform create_source_register_table_for_attribute_partitioning(1523);
perform create_source_register_table_for_attribute_partitioning(1524);
perform create_source_register_table_for_attribute_partitioning(1525);
perform create_source_register_table_for_attribute_partitioning(1526);
perform create_source_register_table_for_attribute_partitioning(1527);
perform create_source_register_table_for_attribute_partitioning(1528);
perform create_source_register_table_for_attribute_partitioning(1529);
perform create_source_register_table_for_attribute_partitioning(1530);
perform create_source_register_table_for_attribute_partitioning(1531);
perform create_source_register_table_for_attribute_partitioning(1532);
perform create_source_register_table_for_attribute_partitioning(1533);
perform create_source_register_table_for_attribute_partitioning(1534);
perform create_source_register_table_for_attribute_partitioning(1535);
perform create_source_register_table_for_attribute_partitioning(1536);
perform create_source_register_table_for_attribute_partitioning(1537);
perform create_source_register_table_for_attribute_partitioning(1538);
perform create_source_register_table_for_attribute_partitioning(1539);
perform create_source_register_table_for_attribute_partitioning(1540);
perform create_source_register_table_for_attribute_partitioning(1541);
perform create_source_register_table_for_attribute_partitioning(1542);
perform create_source_register_table_for_attribute_partitioning(1543);
perform create_source_register_table_for_attribute_partitioning(1544);
perform create_source_register_table_for_attribute_partitioning(1545);
perform create_source_register_table_for_attribute_partitioning(1546);
perform create_source_register_table_for_attribute_partitioning(1547);
perform create_source_register_table_for_attribute_partitioning(1548);
perform create_source_register_table_for_attribute_partitioning(1549);
perform create_source_register_table_for_attribute_partitioning(1550);
perform create_source_register_table_for_attribute_partitioning(1551);
perform create_source_register_table_for_attribute_partitioning(1552);
perform create_source_register_table_for_attribute_partitioning(1553);
perform create_source_register_table_for_attribute_partitioning(1554);
perform create_source_register_table_for_attribute_partitioning(1555);
perform create_source_register_table_for_attribute_partitioning(1556);
perform create_source_register_table_for_attribute_partitioning(1557);
perform create_source_register_table_for_attribute_partitioning(1558);
perform create_source_register_table_for_attribute_partitioning(1559);
perform create_source_register_table_for_attribute_partitioning(1560);
perform create_source_register_table_for_attribute_partitioning(1561);
perform create_source_register_table_for_attribute_partitioning(1562);
perform create_source_register_table_for_attribute_partitioning(1563);
perform create_source_register_table_for_attribute_partitioning(1564);
perform create_source_register_table_for_attribute_partitioning(1565);
perform create_source_register_table_for_attribute_partitioning(1566);
perform create_source_register_table_for_attribute_partitioning(1567);
perform create_source_register_table_for_attribute_partitioning(1568);
perform create_source_register_table_for_attribute_partitioning(1569);
perform create_source_register_table_for_attribute_partitioning(1570);
perform create_source_register_table_for_attribute_partitioning(1571);
perform create_source_register_table_for_attribute_partitioning(1572);
perform create_source_register_table_for_attribute_partitioning(1573);
perform create_source_register_table_for_attribute_partitioning(1574);
perform create_source_register_table_for_attribute_partitioning(1575);
perform create_source_register_table_for_attribute_partitioning(1576);
perform create_source_register_table_for_attribute_partitioning(1577);
perform create_source_register_table_for_attribute_partitioning(1578);
perform create_source_register_table_for_attribute_partitioning(1579);
perform create_source_register_table_for_attribute_partitioning(1580);
perform create_source_register_table_for_attribute_partitioning(1581);
perform create_source_register_table_for_attribute_partitioning(1582);
perform create_source_register_table_for_attribute_partitioning(1583);
perform create_source_register_table_for_attribute_partitioning(1584);
perform create_source_register_table_for_attribute_partitioning(1585);
perform create_source_register_table_for_attribute_partitioning(1586);
perform create_source_register_table_for_attribute_partitioning(1587);
perform create_source_register_table_for_attribute_partitioning(1588);
perform create_source_register_table_for_attribute_partitioning(1589);
perform create_source_register_table_for_attribute_partitioning(1590);
perform create_source_register_table_for_attribute_partitioning(1591);
perform create_source_register_table_for_attribute_partitioning(1592);
perform create_source_register_table_for_attribute_partitioning(1593);
perform create_source_register_table_for_attribute_partitioning(1594);
perform create_source_register_table_for_attribute_partitioning(1595);
END $$;