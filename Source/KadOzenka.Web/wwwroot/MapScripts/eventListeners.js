function ChangeBounds(event) {
    const params = new window.URLSearchParams(window.location.search);

    const newCenter = event.get('newCenter');
    params.set('center', newCenter);
    const newZoom = event.get('newZoom');
    params.set('zoom', newZoom);
    if (window.history.replaceState) {
        const newUrl = new URL(window.location.href);
        newUrl.search = params;
        window.history.replaceState({ path: newUrl.href }, '', newUrl.href);
    }
    
    const newBounds = event.get('newBounds');
    refreshCurrentToken();
    GetClusterData(newBounds, newZoom, currentToken, params.has('objectId') ? params.get('objectId') : null);
};

function clickOnCluster(event) {
    var ids = [];
    switch (event.get('target').options._name) {
        case ObjectTypes.cluster:
            event.get('target').properties.get('geoObjects').forEach(x => ids.push(x.properties.get('data')[0].id));
            break;
        case ObjectTypes.geoObject:
            ids.push(event.get('target').properties.get('data')[0].id);
            break;
    }
    createTargetMarker(event.get('target').geometry._coordinates, map.action.getCurrentState().zoom, ids);
    GetRequiredInfo(ids);
};

function changeObjectsCount(zoom, count) {
    document.getElementById("CountControl").classList.add("loaded");
    document.getElementById("CountControlText").innerHTML = `Объектов в видимой области ${count}`;
};

function redrawWaiting() {
    if (document.getElementById("CountControl")) {
        document.getElementById("CountControl").classList.remove("loaded");
        document.getElementById("CountControlText").innerHTML = `Загрузка объектов`;
    }
};

function removeTarget(placemark) {
    clearCardContainer();
    removeTargetWidget();
    removeTargetMarker(placemark);
    clusterSelected = null;
};

function toTarget() { if (clusterSelected) map.setCenter(clusterSelected.coords, clusterSelected.zoom, "map"); };

function changeMapType(type, element) {
    Array.from(document.getElementsByClassName("filterHeader")).forEach(x => { if (x.id != element.id) x.classList.remove("active"); });
    element.classList.toggle("active");
    changeLayer(element.classList.contains("active") ? type : 0);
}

function changeLayer(type) {
    if(type != null) currentLayer = type;
    for (var i = 0; i < CLD.length; i++) { map.geoObjects.remove(CLD[i]); }
    CLD = [];
    switch (currentLayer) {
        case MapZoneType.district:
            addDistrictsLayer();
            break;
        case MapZoneType.region:
            addAreaLayer();
            break;
        case MapZoneType.zone:
            addZoneLayer();
            break;
        case MapZoneType.quartal:
            addQuartalLayer();
            break;
        default:
            break;
    }
}