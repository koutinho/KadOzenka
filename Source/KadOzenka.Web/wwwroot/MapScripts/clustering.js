function initCluster(coordinates) {

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
                data: [{weight: 1, color: color, id: AllControllersData[i].id}]
            }, {
                iconColor: color,
                iconLayout: ClusterSettings.layout,
                iconPieChartRadius: ClusterSettings.pieChartRadius,
                iconPieChartCoreRadius: ClusterSettings.pieChartCoreRadius,
                iconPieChartStrokeWidth: ClusterSettings.pieChartStrokeWidth
            });
    }
    clusterer.add(geoObjects);
	clusterer.events.add('click', function (e) { clickOnCluster(e); });
	map.geoObjects.removeAll();
	map.geoObjects.add(clusterer);

	const url = new URL(window.location);
	const params = new window.URLSearchParams(url.search);
	if (!params.has('center')) {
		map.setBounds(clusterer.getBounds(), { checkZoomRange: true });
	}
}

function clickOnCluster(e) {
    var ids = [];
    switch (e.get('target').options._name) {
        case ObjectTypes.cluster:
            e.get('target').properties.get('geoObjects').forEach(x => ids.push(x.properties.get('data')[0].id));
            break;
        case ObjectTypes.geoObject:
            ids.push(e.get('target').properties.get('data')[0].id);
            break;
    }
    GetRequiredInfo(ids);
}