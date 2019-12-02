function BackTo(event) { window.location.href = "/"; };

function ChangeBounds(event) {
    const params = new window.URLSearchParams(window.location.search);

    let browserCenter = null, browserZoom = null;
    if (params.has('center') && params.has('zoom')) {
        browserCenter = params.get('center').split(",");
        browserZoom = params.get('zoom');
    }

    const newCenter = event.get('newCenter');
    params.set('center', newCenter);
    const newZoom = event.get('newZoom');
    params.set('zoom', newZoom);
    if (window.history.pushState) {
        if (browserCenter && browserZoom) {
            if ((+browserCenter[0]).toFixed(10) !== newCenter[0].toFixed(10) ||
                (+browserCenter[1]).toFixed(10) !== newCenter[1].toFixed(10) ||
                (+browserZoom) !== newZoom) {
                const newUrl = new URL(window.location.href);
                newUrl.search = params;
                window.history.pushState({ path: newUrl.href }, '', newUrl.href);
            }
        } else {
            const newUrl = new URL(window.location.href);
            newUrl.search = params;
            window.history.pushState({ path: newUrl.href }, '', newUrl.href);
        }
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
    removeTargetWidget();
    removeTargetMarker(placemark);
    clusterSelected = null;
};

function toTarget() { if (clusterSelected) map.setCenter(clusterSelected.coords, clusterSelected.zoom, "map"); };