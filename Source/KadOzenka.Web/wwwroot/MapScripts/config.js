var AppData = {
    protocol: "https",
    version: "2.1",
    lang: "ru_RU",
    key: "5400667f-f671-4f5b-a680-1296945f82e5",
    defaultRemoveElements: ["trafficControl", "geolocationControl", "fullscreenControl", "zoomControl"]
};

var MapSettings = {
    containerId: "map",
    center: [55.76, 37.64],
    zoom: 9,
    minClusterZoom: 15,
    maxLoadedObjectsCount: 1,
    leftMenuMaxValues: 20
};

var ClusterSettings = {
    layout: 'default#pieChart',     // Макет метки кластера pieChart.
    pieChartRadius: 20,             // Радиус диаграммы в пикселях.
    pieChartCoreRadius: 15,         // Радиус центральной части макета.
    pieChartStrokeWidth: 2,         // Ширина линий-разделителей секторов и внешней обводки диаграммы.
    balloon: false,                 // Определяет наличие поля balloon.
    disableClickZoom: true          // Запрет зума кластера по клику
};

var MapWithDefinedObjectSettings = {
    zoom: 17,
    iconLayout: 'default#image',
    iconImageHref: '../images/mapObjectLogoWithPin.png',
    iconImageSize: [50, 50],
    iconImageOffset: [-25, -50]
}

var SelectedClusterSettings = {
	clusterIconHref: 'MapIcons/home.svg',
	clusterIconSize: [40, 40],
	clusterIconOffset: [-20, -20]
};

var SelectedGeoObjectSettings = {
	iconLayout: 'default#image',
	iconImageHref: 'MapIcons/home.svg',
	iconImageSize: [30, 30],
	iconImageOffset: [-15, -15]
};

var SelectedTargetWidget = {
    iconLayout: 'default#image',
    iconImageHref: 'images/mapObjectLogo.png',
    iconImageSize: [50, 50],
    iconImageOffset: [-25, -25]
}

var SelectedTargetOnCard = {
    iconLayout: 'default#image',
    iconImageHref: '../images/mapObjectLogo.png',
    iconImageSize: [50, 50],
    iconImageOffset: [-25, -25]
}

var PropType = [
    { id: 0, name: 'warehouse', color: '#5206af', type: 'Склад' }, 
    { id: 1, name: 'parking', color: '#0d2db0', type: 'Гараж' }, 
    { id: 2, name: 'trading', color: '#0177a1', type: 'Торговая' }, 
    { id: 3, name: 'free', color: '#8341d7', type: 'Свободного назначения' },
    { id: 4, name: 'office', color: '#4762d7', type: 'Офис' }, 
    { id: 5, name: 'business', color: '#9a6ad7', type: 'Готовый бизнес' }, 
    { id: 6, name: 'production', color: '#6f82d7', type: 'Производственная' }, 
    { id: 7, name: 'uncompleted', color: '#acadad', type: 'ОНС' }, 
    { id: 8, name: 'building', color: '#37a8d1', type: 'Здание' }, 
    { id: 9, name: 'otherCommertial', color: '#62b3d1', type: 'Иная коммерческая недвижимость' }, 
    { id: 10, name: 'flatSecondary', color: '#007730', type: 'Квартира' }, 
    { id: 11, name: 'flatNew', color: '#1c894e', type: 'Квартира' }, 
    { id: 12, name: 'roomSecondary', color: '#00b74e', type: 'Комната на вторичке' }, 
    { id: 13, name: 'roomNew', color: '#34db7f', type: 'Комната в новостройке' }, 
    { id: 14, name: 'subarbanArea', color: '#a75000', type: 'Земельный участок' }, 
    { id: 15, name: 'townhouse', color: '#c0772c', type: 'Таунхаус' }, 
    { id: 16, name: 'house', color: '#ff7d00', type: 'Загородный дом' }, 
    { id: 17, name: 'otherSubarban', color: '#ffb874', type: 'Иной вид недвижимости' }, 
    { id: 18, name: 'studio', color: '#62db98', type: 'Квартира' }
];

var SegmentType = [
    { id: 0, name: 'appartment', color: '#a68a90', type: 'Апартаменты' },
    { id: 1, name: 'box', color: '#d95891', type: 'Гаражи' },
    { id: 2, name: 'hotel', color: '#54f5db', type: 'Гостиницы' },
    { id: 3, name: 'IZHS', color: '#417c55', type: 'ИЖС' },
    { id: 4, name: 'parking', color: '#872187', type: 'Машиноместа' },
    { id: 5, name: 'MZHS', color: '#cb9d2c', type: 'МЖС' },
    { id: 6, name: 'Offices', color: '#471942', type: 'Офисы' },
    { id: 7, name: 'Warehouse', color: '#dcc209', type: 'Производство и склады' },
    { id: 8, name: 'Gardens', color: '#b40495', type: 'Садоводческое, огородническое и дачное использование' },
    { id: 9, name: 'Sanatorium', color: '#b6b369', type: 'Санатории' },
    { id: 10, name: 'Traiding', color: '#d27f4d', type: 'Торговля' },
    { id: 11, name: 'publicCatering', color: '#1b62da', type: 'Общепит' },
    { id: 12, name: 'OrdinarLand', color: '#e997ae', type: 'Земельные участки' },
    { id: 13, name: 'CommercialLand', color: '#67a3a0', type: 'Коммерческая земля' },
    { id: 14, name: 'NoSegment', color: '#c4dc66', type: 'Без сегмента' }
];

var ObjectTypes = {
    cluster: "cluster",
    geoObject: "geoObject"
};

var geoTagSlicer = {
    startIndex: 0,
    countIndex: 20
};

var countWidgetPosition = {
    bottom: 10,
    left: 10
}

var filterWidgetPosition = {
    top: 48,
    left: 10
}

var layerWidgetPosition = {
    top: 96,
    left: 10
}

var targetWidgetPosition = {
    bottom: 48,
    left: 10
}

const MapZoneType = { "district": 1, "region": 2, "zone": 3, "quartal": 4 };
Object.freeze(MapZoneType);

var targetWidget = null;
var clusterSelected = null;
var currentToken = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
var currentLayer = null;
var CLD = [];