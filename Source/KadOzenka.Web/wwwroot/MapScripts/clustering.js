function initCluster(coordinates, zoom) {
	map.geoObjects.removeAll();

    console.log(coordinates.length);
    if (zoom >= 15) {
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
    else
    {
        console.log(coordinates.length);
        map.geoObjects.removeAll();
    }
}