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

function changeObjectsCount(count) {
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

function refreshFilterWidget(filterInfo) {
    var dealTypeData = '', marketSegmentData = '';
    filterInfo.dealTypeList.forEach(x => { dealTypeData += `<div id="${x.Name}FilterButton" class="filterBodyButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`; });
    filterInfo.marketSegmentList.forEach(x => { marketSegmentData += `<div id="${x.Name}FilterButton" class="filterBodyButton">${x.Value.replace(new RegExp(' ', 'g'), '&nbsp;')}</div>`; });
    document.getElementById('dealTypefilterBody').innerHTML = dealTypeData;
    document.getElementById('marketSegmentfilterBody').innerHTML = marketSegmentData;
};

function toTarget() { if (clusterSelected) map.setCenter(clusterSelected.coords, clusterSelected.zoom, "map"); };

function changeMapType(type, element) {
    Array.from(document.getElementsByClassName("filterHeader")).forEach(x => { if (x.id != element.id) x.classList.remove("active"); });
    element.classList.toggle("active");
    changeLayer(element.classList.contains("active") ? type : 0);
}

function changeLayer(type) {
    if(type != null) currentLayer = type;
    map.geoObjects.remove(SOM);
    switch (currentLayer) {
        case MapZoneType.district:
            setCurrentLayer('/MapJSONData/districts.min.json');
            break;
        case MapZoneType.region:
            setCurrentLayer('/MapJSONData/regions.min.json');
            break;
        case MapZoneType.zone:
            setCurrentLayer('/MapJSONData/zones.min.json');
            break;
        case MapZoneType.quartal:
            setCurrentLayer('/MapJSONData/quartal.min.json');
            break;
    }
}

function setCurrentLayer(url) {
    SOM = new ymaps.ObjectManager();
    $.getJSON(url).done(function (geoJson) {
        geoJson.features.forEach(function (obj) { try { obj.properties.balloonContent = obj.properties.description; } catch{ } });
        SOM.add(geoJson);
        map.geoObjects.add(SOM);
    });
}