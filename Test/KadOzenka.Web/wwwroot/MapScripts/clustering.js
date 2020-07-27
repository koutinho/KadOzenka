function initCluster(coordinates, zoom, size) {
    map.geoObjects.remove(clustererData);
    if (pointsData) pointsData.forEach(x => { map.geoObjects.remove(x); });
    if (zoom >= MapSettings.minClusterZoom || size <= MapSettings.maxObjectsCount) {
        clustererData = new ymaps.Clusterer({
            clusterIconLayout: ClusterSettings.layout,
            clusterIconPieChartRadius: ClusterSettings.pieChartRadius,
            clusterIconPieChartCoreRadius: ClusterSettings.pieChartCoreRadius,
            clusterIconPieChartStrokeWidth: ClusterSettings.pieChartStrokeWidth,
            hasBalloon: ClusterSettings.balloon,
            clusterDisableClickZoom: ClusterSettings.disableClickZoom
        }),
        AllControllersData = coordinates,
        geoObjects = [];
        const url = new URL(window.location);
        const params = new window.URLSearchParams(url.search);
        let geoObject = null;
        if (params.has('objectId')) {
            var id = params.get('objectId');
            AllControllersData = AllControllersData.filter(function (item) {
                if (item.id == id) {
                    window.history.replaceState({}, document.title, "/Map?" + window.location.href.split('?')[1].replace(/objectId=.*?&/, ''));
                    arr = [id];
                    geoObject = new ymaps.Placemark(item.points, { data: [{ id: item.id }] }, SelectedTargetWidget);
                    clusterSelected = { geoObject: geoObject, coords: item.points, zoom: MapSettings.minClusterZoom };
                    createTargetMarker(item.points, MapWithDefinedObjectSettings.zoom, arr);
                    GetRequiredInfo(arr);
                    geoObject.events.add('click', function (event) { removeTarget(geoObject); });
                }
                return item.id != id;
            });
        }
        for (var i = 0, len = AllControllersData.length; i < len; i++) {
            var color = CIPJSType[AllControllersData[i].segment][AllControllersData[i].dealType].color;
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
        clustererData.add(geoObjects);
        clustererData.events.add('click', function (event) { clickOnCluster(event); });
        map.geoObjects.add(clustererData);
    }
    else {
        pointsData = [];
        coordinates.forEach(x => pointsData.push(new ymaps.Placemark(x.points, {}, PointsGeoObjectSettings)));
        pointsData.forEach(x => { map.geoObjects.add(x); });
        console.log(pointsData.length);
    }
    if (clusterSelected) map.geoObjects.add(clusterSelected.geoObject);
};

function createTargetMarker(coords, zoom, ids) {
    if (clusterSelected) map.geoObjects.remove(clusterSelected.geoObject);
    geoObject = new ymaps.Placemark(coords, { data: [{ id: ids }] }, SelectedTargetWidget);
    clusterSelected = { geoObject: geoObject, coords: coords, zoom: zoom };
    geoObject.events.add('click', function (event) { removeTarget(geoObject); });
    addTargetWidget(targetWidgetPosition);
    map.geoObjects.add(geoObject);
    map.setCenter(coords, zoom, "map");
};

function removeTargetMarker(placemark) {
    map.geoObjects.remove(placemark);
    placemark = null;
};