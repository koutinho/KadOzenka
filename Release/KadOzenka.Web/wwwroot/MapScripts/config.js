var AppData = {
    protocol: "https",
    version: "2.1",
    lang: "ru_RU",
    key: "5b7a0369-63be-4edd-ac27-716d52c64d46",
    defaultRemoveElements: ["trafficControl", "geolocationControl", "fullscreenControl", "zoomControl"],
    ProxyIp: "10.80.214.37:8888"
};

var MapSettings = {
    containerId: "map",
    center: [55.76, 37.64],
    zoom: 9,
    minClusterZoom: 15,
    maxLoadedObjectsCount: 500,
    maxObjectsCount: 7500,
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

var ManagementDecisionSupportMapSettings = {
    containerId: "map",
    center: [55.76, 37.64],
    zoom: 13
};

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

var PointsGeoObjectSettings = {
    iconLayout: 'default#image',
    iconImageHref: 'MapIcons/mapDot.svg',
    iconImageSize: [7, 7],
    iconImageOffset: [0, 0]
}

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

var CIPJSType = [
    [
        { id: 0, name: 'IZHS', color: '#045C1F', type: 'ИЖС', dealType: 'Предложение-продажа' },
        { id: 1, name: 'IZHS', color: '#078B2E', type: 'ИЖС', dealType: 'Сделка купли-продажи' },
        { id: 2, name: 'IZHS', color: '#4BAF69', type: 'ИЖС', dealType: 'Предложение-аренда' },
        { id: 3, name: 'IZHS', color: '#A5D7B4', type: 'ИЖС', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 4, name: 'MZHS', color: '#00663B', type: 'МЖС', dealType: 'Предложение-продажа' },
        { id: 5, name: 'MZHS', color: '#00B268', type: 'МЖС', dealType: 'Сделка купли-продажи' },
        { id: 6, name: 'MZHS', color: '#00FF95', type: 'МЖС', dealType: 'Предложение-аренда' },
        { id: 7, name: 'MZHS', color: '#66FFBF', type: 'МЖС', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 8, name: 'Appartment', color: '#006166', type: 'Апартаменты', dealType: 'Предложение-продажа' },
        { id: 9, name: 'Appartment', color: '#00AAB2', type: 'Апартаменты', dealType: 'Сделка купли-продажи' },
        { id: 10, name: 'Appartment', color: '#32F5FF', type: 'Апартаменты', dealType: 'Предложение-аренда' },
        { id: 11, name: 'Appartment', color: '#CCFCFF', type: 'Апартаменты', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 12, name: 'Sanatorium', color: '#00224C', type: 'Санатории', dealType: 'Предложение-продажа' },
        { id: 13, name: 'Sanatorium', color: '#0050B2', type: 'Санатории', dealType: 'Сделка купли-продажи' },
        { id: 14, name: 'Sanatorium', color: '#0073FF', type: 'Санатории', dealType: 'Предложение-аренда' },
        { id: 15, name: 'Sanatorium', color: '#7Fb9FF', type: 'Санатории', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 16, name: 'Gardens', color: '#4C004A', type: 'Садовническое, огородническое и дачное использование', dealType: 'Предложение-продажа' },
        { id: 17, name: 'Gardens', color: '#990095', type: 'Садовническое, огородническое и дачное использование', dealType: 'Сделка купли-продажи' },
        { id: 18, name: 'Gardens', color: '#FF00F9', type: 'Садовническое, огородническое и дачное использование', dealType: 'Предложение-аренда' },
        { id: 19, name: 'Gardens', color: '#FFD2FD', type: 'Садовническое, огородническое и дачное использование', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 20, name: 'Parking', color: '#66003A', type: 'Машиноместа', dealType: 'Предложение-продажа' },
        { id: 21, name: 'Parking', color: '#B20066', type: 'Машиноместа', dealType: 'Сделка купли-продажи' },
        { id: 22, name: 'Parking', color: '#FF0093', type: 'Машиноместа', dealType: 'Предложение-аренда' },
        { id: 23, name: 'Parking', color: '#FF99D3', type: 'Машиноместа', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 24, name: 'Box', color: '#4C000B', type: 'Гаражи', dealType: 'Предложение-продажа' },
        { id: 25, name: 'Box', color: '#B2001A', type: 'Гаражи', dealType: 'Сделка купли-продажи' },
        { id: 26, name: 'Box', color: '#FF0026', type: 'Гаражи', dealType: 'Предложение-аренда' },
        { id: 27, name: 'Box', color: '#FF7F92', type: 'Гаражи', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 28, name: 'Hotel', color: '#4F1200', type: 'Гостиницы', dealType: 'Предложение-продажа' },
        { id: 29, name: 'Hotel', color: '#BF2D00', type: 'Гостиницы', dealType: 'Сделка купли-продажи' },
        { id: 30, name: 'Hotel', color: '#FF3A00', type: 'Гостиницы', dealType: 'Предложение-аренда' },
        { id: 31, name: 'Hotel', color: '#FF9C7F', type: 'Гостиницы', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 32, name: 'Warehouse', color: '#661700', type: 'Производство и склады', dealType: 'Предложение-продажа' },
        { id: 33, name: 'Warehouse', color: '#CC2E00', type: 'Производство и склады', dealType: 'Сделка купли-продажи' },
        { id: 34, name: 'Warehouse', color: '#FF6132', type: 'Производство и склады', dealType: 'Предложение-аренда' },
        { id: 35, name: 'Warehouse', color: '#FFB099', type: 'Производство и склады', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 36, name: 'Office', color: '#4C2C00', type: 'Офисы', dealType: 'Предложение-продажа' },
        { id: 37, name: 'Office', color: '#995900', type: 'Офисы', dealType: 'Сделка купли-продажи' },
        { id: 38, name: 'Office', color: '#FF9500', type: 'Офисы', dealType: 'Предложение-аренда' },
        { id: 39, name: 'Office', color: '#FFCA7F', type: 'Офисы', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 40, name: 'Traiding', color: '#664F00', type: 'Торговля', dealType: 'Предложение-продажа' },
        { id: 41, name: 'Traiding', color: '#CC9F00', type: 'Торговля', dealType: 'Сделка купли-продажи' },
        { id: 42, name: 'Traiding', color: '#FFC700', type: 'Торговля', dealType: 'Предложение-аренда' },
        { id: 43, name: 'Traiding', color: '#FFE899', type: 'Торговля', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 44, name: 'PublicCatering', color: '#666004', type: 'Общепит', dealType: 'Предложение-продажа' },
        { id: 45, name: 'PublicCatering', color: '#B2A807', type: 'Общепит', dealType: 'Сделка купли-продажи' },
        { id: 46, name: 'PublicCatering', color: '#FFF10A', type: 'Общепит', dealType: 'Предложение-аренда' },
        { id: 47, name: 'PublicCatering', color: '#FFFAB5', type: 'Общепит', dealType: 'Сделка-аренда' }
    ],
    [
        { id: 48, name: 'NoSegment', color: '#4C4C4C', type: 'Без сегмента', dealType: 'Предложение-продажа' },
        { id: 49, name: 'NoSegment', color: '#868686', type: 'Без сегмента', dealType: 'Сделка купли-продажи' },
        { id: 50, name: 'NoSegment', color: '#C0C0C0', type: 'Без сегмента', dealType: 'Предложение-аренда' },
        { id: 51, name: 'NoSegment', color: '#DFDFDF', type: 'Без сегмента', dealType: 'Сделка-аренда' }
    ]
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

var imgTileSettings = {
    imgUrlTemplate: 'Map/cadastralTiles?x=%x&y=%y&z=%z',
    tileTransparent: true
}

const MapZoneType = { "district": 1, "region": 2, "zone": 3, "quartal": 4 };
Object.freeze(MapZoneType);

var UseManager = false;

var ids = [];

var targetWidget = null;
var clusterSelected = null;
var currentToken = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
var currentLayer = null;
var SOM = null;
var imgLayer = null;
var clustererData = null;
var pointsData = null;
var avaliableCIPJSTypes = null;
var avaliableSegments = null;
var avaliableStatuses = null;
var heatMapData = null;

var DISTRICTS_DATA = null;
var SOURCE_DATA = null;
var ACTUAL_DATE = null;