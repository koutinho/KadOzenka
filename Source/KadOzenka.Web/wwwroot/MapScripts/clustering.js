function initCluster(coordinates, zoom, dotSize) {
    if (zoom >= MapSettings.minClusterZoom) {
        clusterer = new ymaps.Clusterer({
            clusterIconLayout: ClusterSettings.layout,
            clusterIconPieChartRadius: ClusterSettings.pieChartRadius,
            clusterIconPieChartCoreRadius: ClusterSettings.pieChartCoreRadius,
            clusterIconPieChartStrokeWidth: ClusterSettings.pieChartStrokeWidth,
            hasBalloon: ClusterSettings.balloon,
            clusterDisableClickZoom: ClusterSettings.disableClickZoom
        }),
        AllControllersData = coordinates,
        geoObjects = [];

        for (var i = 0, len = AllControllersData.length; i < len; i++) {
            var color = PropType[AllControllersData[i].type].color;
            geoObjects[i] = new ymaps.Placemark(
                AllControllersData[i].points, {
                data: [{ weight: 1, color: color, id: AllControllersData[i].id }]
            }, {
                iconColor: color,
                iconLayout: ClusterSettings.layout,
                iconPieChartRadius: ClusterSettings.pieChartRadius,
                iconPieChartCoreRadius: ClusterSettings.pieChartCoreRadius,
                iconPieChartStrokeWidth: ClusterSettings.pieChartStrokeWidth
            });
        }
        clusterer.add(geoObjects);
        clusterer.events.add('click', function (event) { clickOnCluster(event); });
        map.geoObjects.removeAll();
        map.geoObjects.add(clusterer);
    }
    else {
        AllControllersData = coordinates;
        map.geoObjects.removeAll();
        for (var i = 0, len = AllControllersData.length; i < len; i++) {
            map.geoObjects.add(new ymaps.Placemark(AllControllersData[i].points,
            {},
            {
                iconLayout: GeoDotSettings.layout,
                iconImageHref: GeoDotSettings.imageHref,
                iconImageSize: dotSize,
                iconImageOffset: GeoDotSettings.imageOffset
            }));
        }
    }
}