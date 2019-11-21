var AppData = {
    protocol: "https",
    version: "2.1",
    lang: "ru_RU",
    key: "059c0fdb-a157-414f-bfbf-64ae681bfd73",
    defaultRemoveElements: ["trafficControl", "geolocationControl", "fullscreenControl"]
};

var MapSettings = {
    containerId: "map",
    center: [55.76, 37.64],
    zoom: 9
};

var ClusterSettings = {
    layout: 'default#pieChart',     // Макет метки кластера pieChart.
    pieChartRadius: 20,             // Радиус диаграммы в пикселях.
    pieChartCoreRadius: 15,         // Радиус центральной части макета.
    pieChartStrokeWidth: 2,         // Ширина линий-разделителей секторов и внешней обводки диаграммы.
    balloon: false,                 // Определяет наличие поля balloon.
    disableClickZoom: true          // Запрет зума кластера по клику
};

var PropType = [
    { id: 0, name: 'warehouse', color: '#DB425A' },     // Склад
    { id: 1, name: 'parking', color: '#4C4DA2' },       // Гараж
    { id: 2, name: 'trading', color: '#00DEAD' },       // Торговая
    { id: 3, name: 'free', color: '#D73AD2' },          // Свободного назначения
    { id: 4, name: 'office', color: '#F8CC4D' },        // Офисная недвижимость
    { id: 5, name: 'business', color: '#F88D00' },      // Готовый бизнес
    { id: 6, name: 'production', color: '#AC646C' },    // Производственная
    { id: 7, name: 'uncompleted', color: '#548FB7' },   // ОНС
    { id: 8, name: 'uncompleted', color: '#000' },      // Здание
    { id: 9, name: 'uncompleted', color: '#fff' },      // Неизвестно
];

var ObjectTypes = {
    cluster: "cluster",
    geoObject: "geoObject"
};

var geoTagSlicer = {
    startIndex: 0,
    countIndex: 20
};